package   ca.pfv.spmf.algorithms.frequentpatterns.efhaum;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import ca.pfv.spmf.tools.MemoryLogger;


/* This file is copyright (c) 2012-2015 Souleymane Zida & Philippe Fournier-Viger
* 
* This file is part of the SPMF DATA MINING SOFTWARE
* (http://www.philippe-fournier-viger.com/spmf).
* 
* SPMF is free software: you can redistribute it and/or modify it under the
* terms of the GNU General Public License as published by the Free Software
* Foundation, either version 3 of the License, or (at your option) any later
* version.
* SPMF is distributed in the hope that it will be useful, but WITHOUT ANY
* WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
* A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with
* SPMF. If not, see <http://www.gnu.org/licenses/>.
*/

/**
 * This is an implementation of the EFIM algorithm for
 * mining high-utility itemsets from a transaction database.
 * More information on the EFIM algorithm can be found in that paper: <br\>
 *
 * @author Souleymane Zida, Philippe Fournier-Viger using some code by Alan Souza
 */
public class AlgoEFHAM {

	/** the set of high-utility itemsets */
    private Itemsets highUtilityItemsets;
    
	/** object to write the output file */
	BufferedWriter writer = null;
	
	/** the number of high-utility itemsets found (for statistics) */
	private int patternCount; 

	/** the start time and end time of the last algorithm execution */
	long startTimestamp;
	long endTimestamp;
	
	/** the minutil threshold */
	int minUtil;
	
	/** if this variable is set to true, some debugging information will be shown */
    final boolean  DEBUG = false;
	
    /** The following variables are the utility-bins array 
	// Recall that each bucket correspond to an item */
    /** utility bin array for sub-tree utility */
	private double[] utilityBinArraySU;   
	/** utility bin array for local utility */
	private double[] utilityBinArrayLU; 
	
	/** a temporary buffer */
	private int [] temp= new int [500];

	//EFAHM plus
    private int[] tempUtil;
	/** The total time spent for performing intersections */
	long timeIntersections;
	/** The total time spent for performing database reduction */
	long timeDatabaseReduction;
	/** The total time spent for identifying promising items */
	long timeIdentifyPromisingItems;
	/** The total time spent for sorting */
	long timeSort;
	/** The total time spent for binary search */
	long timeBinarySearch;
	

	/** an array that map an old item name to its new name */
    int[] oldNameToNewNames;
    /** an array that map a new item name to its old name */
    int[] newNamesToOldNames;
    /** the number of new items */
    int newItemCount;

    /** if true, transaction merging will be performed by the algorithm */
    boolean activateTransactionMerging;

    /** A parameter for transaction merging*/
    final int MAXIMUM_SIZE_MERGING = 1000;
    
    /** number of times a transaction was read */
    long transactionReadingCount;
    /** number of merges */
    long mergeCount;

    /** number of itemsets from the search tree that were considered */
	private long candidateCount;

	/** If true, sub-tree utility pruning will be performed */
	private boolean activateSubtreeUtilityPruning;
    
	/** 
	 * Constructor
	 */
    public AlgoEFHAM() {
         
    }

    /**
     * Run the algorithm
     * @param minUtil  the minimum utility threshold (a positive integer)
     * @param inputPath  the input file path
     * @param outputPath  the output file path to save the result or null if to be kept in memory
     * @param activateTransactionMerging 
     * @param activateSubtreeUtilityPruning 
     * @param maximumTransactionCount
       * @return the itemsets or null if the user choose to save to file
     * @throws IOException if exception while reading/writing to file
     */
    public Itemsets runAlgorithm(int minUtil, String inputPath, String outputPath, int maximumTransactionCount, boolean activateSubtreeUtilityPruning) throws IOException {
    	
    	// reset variables for statistics
    	mergeCount=0;
    	transactionReadingCount=0;
		timeIntersections = 0;
		timeDatabaseReduction = 0;
    	
    	// save parameters about activating or not the optimizations
    	this.activateTransactionMerging = activateTransactionMerging;
    	this.activateSubtreeUtilityPruning = activateSubtreeUtilityPruning;
    	
		// record the start time
		startTimestamp = System.currentTimeMillis();
		
		// read the input file
		Dataset dataset = new Dataset(inputPath, maximumTransactionCount);

		// save minUtil value selected by the user
		this.minUtil = minUtil;
		tempUtil=new int[dataset.getMaxItem() + 1];

		// if the user choose to save to file
		// create object for writing the output file
		if(outputPath != null) {
			writer = new BufferedWriter(new FileWriter(outputPath));
		}else {
			// if the user choose to save to memory
			writer = null;
	        this.highUtilityItemsets = new Itemsets("Itemsets");
		}
		
		// reset the number of itemset found
		patternCount = 0;
		
		// reset the memory usage checking utility
		MemoryLogger.getInstance().reset();
		
		// if in debug mode, show the initial database in the console
		if(DEBUG)
		{
			System.out.println("===== Initial database === ");
			System.out.println(dataset.toString());
		}
		
	    // Scan the database using utility-bin array to calculate the TWU
		// of each item
		useUtilityBinArrayToCalculateLocalUtilityFirstTime(dataset);
        
       // if in debug mode, show the TWU calculated using the utility-bin array
       if(DEBUG) 
       {
	    	System.out.println("===== TWU OF SINGLE ITEMS === ");
			for (int i = 1; i < utilityBinArrayLU.length; i++)
			{
				System.out.println("item : " + i + " twu: " + utilityBinArrayLU[i]);
			}
			System.out.println();
       }

	   	// Now, we keep only the promising items (those having a twu >= minutil)
	   	List<Integer> itemsToKeep = new ArrayList<Integer>();
	   	for(int j=1; j< utilityBinArrayLU.length;j++) {
	   		if(utilityBinArrayLU[j] >= minUtil) {
	   			itemsToKeep.add(j);
	   		}
	   	}
	   	
	  // Sort promising items according to the increasing order of TWU
	   insertionSort(itemsToKeep, utilityBinArrayLU);
       
	   // Rename promising items according to the increasing order of TWU.
	   // This will allow very fast comparison between items later by the algorithm
	   // This structure will store the new name corresponding to each old name
       oldNameToNewNames = new int[dataset.getMaxItem() + 1];
       // This structure will store the old name corresponding to each new name
       newNamesToOldNames = new int[dataset.getMaxItem() + 1];
       // We will now give the new names starting from the name "1"
       int currentName = 1;
       // For each item in increasing order of TWU
       for (int j=0; j< itemsToKeep.size(); j++)
	   {
    	   // get the item old name
    	   int item = itemsToKeep.get(j);
    	   // give it the new name
		   oldNameToNewNames[item] = currentName;
		   // remember its old name
		   newNamesToOldNames[currentName] = item;
		   // replace its old name by the new name in the list of promising items
		   itemsToKeep.set(j, currentName);
		   // increment by one the current name so that 
		   currentName++;
	   }
       
        // remember the number of promising item
		newItemCount = itemsToKeep.size();
		// initialize the utility-bin array for counting the subtree utility
		utilityBinArraySU = new double[newItemCount + 1]; 

		// if in debug mode, print to the old names and new names to the console
		// to check if they are correct
		if (DEBUG) {
			System.out.println(itemsToKeep);
			System.out.println(Arrays.toString(oldNameToNewNames));
			System.out.println(Arrays.toString(newNamesToOldNames));
		}

        // We now loop over each transaction from the dataset
		// to remove unpromising items
    	for(int i=0; i< dataset.getTransactions().size();i++)
    	{
    		// Get the transaction
    		Transaction transaction  = dataset.getTransactions().get(i);

    		// Remove unpromising items from the transaction and at the same time
    		// rename the items in the transaction according to their new names
    		// and sort the transaction by increasing TWU order
    		transaction.removeUnpromisingItems(oldNameToNewNames);
    	}
    	
    	// Now we will sort transactions in the database according to the proposed
    	// total order on transaction (the lexicographical order when transactions
    	// are read backward).
    	long timeStartSorting = System.currentTimeMillis();

    	// record the total time spent for sorting
    	timeSort = System.currentTimeMillis() - timeStartSorting;
    	
    	// if in debug mode, print the database after sorting and removing promising items
		if(DEBUG)
		{
			System.out.println("===== Database without unpromising items and sorted by TWU increasing order === ");
			System.out.println(dataset.toString());
		}
		
		// Use an utility-bin array to calculate the sub-tree utility of each item
    	useUtilityBinArrayToCalculateSubtreeUtilityFirstTime(dataset);
    	
    	// Calculate the set of items that pass the sub-tree utility pruning condition
		List<Integer> itemsToExplore = new ArrayList<Integer>();
		// if subtree utility pruning is activated
		if(activateSubtreeUtilityPruning){
			// for each item
			for(Integer item : itemsToKeep){
				// if the subtree utility is higher or equal to minutil, then keep it
				if (utilityBinArraySU[item] >= minUtil) {
					itemsToExplore.add(item);
				}
			}
    	}

		// If in debug mode, show the list of promising items
    	if(DEBUG)
    	{
	       	System.out.println("===== List of promising items === ");
	       	System.out.println(itemsToKeep);
    	}

//    	//======
        // Recursive call to the algorithm
       	// If subtree utility pruning is activated
    	if(activateSubtreeUtilityPruning){
    		// We call the recursive algorithm with the database, secondary items and primary items
    		backtrackingEFIM(dataset.getTransactions(), itemsToKeep, itemsToExplore, 0);
    	}else{
    		// We call the recursive algorithm with the database and secondary items
    		backtrackingEFIM(dataset.getTransactions(), itemsToKeep, itemsToKeep, 0);
    	}

		// record the end time
		endTimestamp = System.currentTimeMillis();
		
		//close the output file
		if(writer != null) {
			writer.close();
		}
		
		// check the maximum memory usage
		MemoryLogger.getInstance().checkMemory();
        
		// return the set of high-utility itemsets
        return highUtilityItemsets;
    }

	/**
	 * Implementation of Insertion sort for sorting a list of items by increasing order of TWU.
	 * This has an average performance of O(n log n)
	 * @param items list of integers to be sorted
	 * @param items list the utility-bin array indicating the TWU of each item.
	 */
	public static void insertionSort(List<Integer> items, double [] utilityBinArrayTWU){
		// the following lines are simply a modified an insertion sort
		
		for(int j=1; j< items.size(); j++){
			Integer itemJ = items.get(j);
			int i = j - 1;
			Integer itemI = items.get(i);
			
			// we compare the twu of items i and j
			double comparison = utilityBinArrayTWU[itemI] - utilityBinArrayTWU[itemJ];
			// if the twu is equal, we use the lexicographical order to decide whether i is greater
			// than j or not.
			if(comparison == 0){
				comparison = itemI - itemJ;
			}
			
			while(comparison > 0){
				items.set(i+1, itemI);

				i--;
				if(i<0){
					break;
				}
				
				itemI = items.get(i);
				comparison = utilityBinArrayTWU[itemI] - utilityBinArrayTWU[itemJ];
				// if the twu is equal, we use the lexicographical order to decide whether i is greater
				// than j or not.
				if(comparison == 0){
					comparison = itemI - itemJ;
				}
			}
			items.set(i+1,itemJ);
		}
	}
    
    /**
     * Recursive method to find all high-utility itemsets
     * @param the list of transactions containing the current prefix P
	 * @param itemsToKeep the list of secondary items in the p-projected database
	 * @param itemsToExplore the list of primary items in the p-projected database
	 * @param the current prefixLength
     * @throws IOException if error writing to output file
     */
    private void backtrackingEFIM( List<Transaction> transactionsOfP,
    		List<Integer> itemsToKeep, List<Integer> itemsToExplore, int prefixLength) throws IOException {

    	// update the number of candidates explored so far
		candidateCount += itemsToExplore.size();
    	
        // ========  for each frequent item  e  =============
		for (int j = 0; j < itemsToExplore.size(); j++) {
			Integer e = itemsToExplore.get(j);

			// ========== PERFORM INTERSECTION =====================
			// Calculate transactions containing P U {e} 
			// At the same time project transactions to keep what appears after "e"
	        List<Transaction> transactionsPe = new ArrayList<Transaction>();
	        
	        // variable to calculate the utility of P U {e}
			int utilityPe = 0;		

			// For merging transactions, we will keep track of the last transaction read
			// and the number of identical consecutive transactions
			Transaction previousTransaction = null;
			
	        // this variable is to record the time for performing intersection
			long timeFirstIntersection = System.currentTimeMillis();
			
			// For each transaction
	        for(Transaction transaction : transactionsOfP) {
	        	// Increase the number of transaction read
	        	transactionReadingCount++;
	        	
	        	// To record the time for performing binary searh
	        	long timeBinaryLocal = System.currentTimeMillis();
	        	
	        	// we remember the position where e appears.
	        	// we will call this position an "offset"
	        	int positionE = -1;
	        	// Variables low and high for binary search
	    		int low = transaction.offset;
	    		int high = transaction.items.length - 1;

	    		// perform binary search to find e in the transaction
	    		while (high >= low ) {
	    			int middle = (low + high) >>> 1; // divide by 2
	    			if (transaction.items[middle] < e) {
	    				low = middle + 1;
	    			}else if (transaction.items[middle] == e) {
	    				positionE =  middle;
	    				break;
	    			}  else{
	    				high = middle - 1;
	    			}
	    		}
	    		// record the time spent for performing the binary search
	        	timeBinarySearch +=  System.currentTimeMillis() - timeBinaryLocal;
	        	
	        	// if 'e' was found in the transaction
	            if (positionE > -1  ) { 
	 
	            	// optimization: if the 'e' is the last one in this transaction,
	            	// we don't keep the transaction
					if(transaction.getLastPosition() == positionE){
						// but we still update the sum of the utility of P U {e}
						utilityPe  += transaction.utilities[positionE] + transaction.prefixUtility;
					}else{
						// otherwise
		            		Transaction projectedTransaction = new Transaction(transaction, positionE);
							// we add the utility of Pe in that transaction to the total utility of Pe
							utilityPe  += projectedTransaction.prefixUtility;
							// we put the projected transaction in the projected database of Pe
							transactionsPe.add(projectedTransaction);
						}
					// This is an optimization for binary search:
					// we remember the position of E so that for the next item, we will not search
					// before "e" in the transaction since items are visited in lexicographical order
		            transaction.offset = positionE;   
	            }else{
					// This is an optimization for binary search:
					// we remember the position of E so that for the next item, we will not search
					// before "e" in the transaction since items are visited in lexicographical order
	            	transaction.offset = low;
	            }
	        }
	        // remember the total time for peforming the database projection
	        timeIntersections += (System.currentTimeMillis() - timeFirstIntersection);

	        // Append item "e" to P to obtain P U {e}
	        // but at the same time translate from new name of "e"  to its old name
	        temp[prefixLength] = newNamesToOldNames[e];
     
	        // if the utility of PU{e} is enough to be a high utility itemset
	        if(utilityPe / (prefixLength + 1.0)  >= minUtil)
	        {
	        	// output PU{e}
	        	output(prefixLength, utilityPe );
	        }

			//==== Next, we will calculate the Local Utility and Sub-tree utility of
	        // all items that could be appended to PU{e} ====
	        useUtilityBinArraysToCalculateUpperBounds(transactionsPe, j, itemsToKeep, prefixLength+1);  
			
	        // we now record time for identifying promising items
			long initialTime = System.currentTimeMillis();
			
			// We will create the new list of secondary items
			List<Integer> newItemsToKeep = new ArrayList<Integer>();
			// We will create the new list of primary items
			List<Integer> newItemsToExplore = new ArrayList<Integer>();
			
			// for each item
	    	for (int k = j+1; k < itemsToKeep.size(); k++) {
	        	Integer itemk =  itemsToKeep.get(k);
	        	
	        	// if the sub-tree utility is no less than min util
	            if(utilityBinArraySU[itemk] >= minUtil) {
	            	// and if sub-tree utility pruning is activated
	            	if(activateSubtreeUtilityPruning){
	            		// consider that item as a primary item
	            		newItemsToExplore.add(itemk);
	            	}
	            	// consider that item as a secondary item
	            	newItemsToKeep.add(itemk);
	            }else if(utilityBinArrayLU[itemk] >= minUtil)
	            {
	            	// otherwise, if local utility is no less than minutil,
	            	// consider this itemt to be a secondary item
	            	newItemsToKeep.add(itemk);
	            }
	        }
	    	// update the total time  for identifying promising items
	    	timeIdentifyPromisingItems +=  (System.currentTimeMillis() -  initialTime);
			
			// === recursive call to explore larger itemsets
	    	if(activateSubtreeUtilityPruning){
	    		// if sub-tree utility pruning is activated, we consider primary and secondary items
	    		backtrackingEFIM(transactionsPe, newItemsToKeep, newItemsToExplore,prefixLength+1);
	    	}else{
	    		// if sub-tree utility pruning is deactivated, we consider secondary items also
	    		// as primary items
	    		backtrackingEFIM(transactionsPe, newItemsToKeep, newItemsToKeep,prefixLength+1);
	    	}
		}

		// check the maximum memory usage for statistics purpose
		MemoryLogger.getInstance().checkMemory();
    }


    	/**
	 * Scan the initial database to calculate the local utility of each item
	 * using a utility-bin array
	 * @param dataset the transaction database
	 */
	public void useUtilityBinArrayToCalculateLocalUtilityFirstTime(Dataset dataset) {

		// Initialize utility bins for all items
		utilityBinArrayLU = new double[dataset.getMaxItem() + 1];

		// Scan the database to fill the utility bins
		// For each transaction
		for (Transaction transaction : dataset.getTransactions()) {
			// for each item
			for(Integer item: transaction.getItems()) {
				// we add the transaction utility to the utility bin of the item
				utilityBinArrayLU[item] += transaction.transactionAUtility;
			}
		}
	}
	
	/**
	 * Scan the initial database to calculate the sub-tree utility of each item
	 * using a utility-bin array
	 * @param dataset the transaction database
	 */
	public void useUtilityBinArrayToCalculateSubtreeUtilityFirstTime(Dataset dataset) {
         double AUtility=0;
		// Scan the database to fill the utility-bins of each item
		// For each transaction
		for (Transaction transaction : dataset.getTransactions()) {
			// We will scan the transaction backward. Thus,
			// the current sub-tree utility in that transaction is zero 
			// for the last item of the transaction.
			int len = 0;

			// For each item when reading the transaction backward
    	   for(int i = transaction.getItems().length-1; i >=0; i--) {
    		   // get the item
    		   Integer item = transaction.getItems()[i];

    		   //=================modify according to EFHAM plus
   		    // we add the utility of the current item to its sub-tree utility
    		   AUtility= transaction.getUtilities()[i];
      		//   utilityBinArraySU[item] += transaction.getUtilities()[i];
      		   int j=0;
      		   // we add the utility of the maxExtended 
      		   for (; j<len;++j){
      			  if (tempUtil[j]>=transaction.getUtilities()[i]){
      				  AUtility+=tempUtil[j];
      			  }
      			  else 
      				  break;
      		   }
      		   utilityBinArraySU[item]+=AUtility/(j+1.0);
      		   len=sortedInsert(tempUtil,len,transaction.getUtilities()[i]);
      		   //=========================================================
       		} 		   
		}
	}
private int sortedInsert(int[] a,int nElems,int item ){
	if (nElems==0) {
		a[0] = item;
	    return 1;
	}
		
	int j = 0;
	
    while (j <nElems  && item < a[j]){ 
    	j++;
    }
  int k=nElems;
  while (k>j){	
      a[k] = a[k - 1];
      k--;
  }
   
  a[j] = item;
  nElems++;
  return nElems; 
	}




    /**
     * Utilize the utility-bin arrays to calculate the sub-tree utility and local utility of all
     * items that can extend itemset P U {e}
     * @param transactions the projected database for P U {e}
     * @param j the position of j in the list of promising items
     * @param itemsToKeep the list of promising items
     */
    private void useUtilityBinArraysToCalculateUpperBounds(List<Transaction> transactionsPe, 
    		int j, List<Integer> itemsToKeep, int prefixLength) {

    	// we will record the time used by this method for statistics purpose
		long initialTime = System.currentTimeMillis();
		// For each promising item > e according to the total order
		for (int i = j + 1; i < itemsToKeep.size(); i++) {
			Integer item = itemsToKeep.get(i);
			// We reset the utility bins of that item for computing the sub-tree utility and
			// local utility
			utilityBinArraySU[item] = 0;
			utilityBinArrayLU[item] = 0;
		}

		// for each transaction
		for (Transaction transaction : transactionsPe) {
			// count the number of transactions read
			transactionReadingCount++;
			int len=0;
			// we set high to the last promising item for doing the binary search
			int high = itemsToKeep.size() - 1;

			// for each item in the transaction that is greater than i when reading the transaction backward
			// Note: >= is correct here. It should not be >.
			for (int i = transaction.getItems().length - 1; i >= transaction.offset; i--) {
				// get the item
				int item = transaction.getItems()[i];
				
				// We will check if this item is promising using a binary search over promising items.

				// This variable will be used as a flag to indicate that we found the item or not using the binary search
				boolean contains = false;
				// we set "low" for the binary search to the first promising item position
				int low = 0;

				// do the binary search
				while (high >= low) {
					int middle = (low + high) >>> 1; // divide by 2
					int itemMiddle = itemsToKeep.get(middle);
					if (itemMiddle == item) {
						// if we found the item, then we stop
						contains = true;
						break;
					} else if (itemMiddle < item) {
						low = middle + 1;
					} else {
						high = middle - 1;
					}  
				}
				// if the item is promising
				if (contains) {
					
						//=========modify according to EFHAM
						// We update the sub-tree utility of that item in its utility-bin
						double AUtility= transaction.getUtilities()[i]+(transaction.prefixUtility*prefixLength);
						int k=0;
			      		for (; k<len;++k){
			      		  if (tempUtil[k]>=(transaction.getUtilities()[i]+(transaction.prefixUtility*prefixLength))/(prefixLength+1)){
		      				  AUtility+=tempUtil[k];
			      		  }
		      			  else 
		      				  break;
			      		}
			      		utilityBinArraySU[item]+=AUtility/(prefixLength+k+1.0);           		    
						// We update the local utility of that item in its utility-bin
	           		    utilityBinArrayLU[item] +=( 
								(transaction.transactionAUtility*len) 
								+
								(transaction.prefixUtility*prefixLength)
								+
								transaction.getUtilities()[i]
								)
								/
								prefixLength+len+1;
								
	           		    len=sortedInsert(tempUtil,len,transaction.getUtilities()[i]);
           		    //============================================================

					}	
					}
		}
		// we update the time for database reduction for statistics purpose
		timeDatabaseReduction += (System.currentTimeMillis() - initialTime);
    }



    /**
     * Save a high-utility itemset to file or memory depending on what the user chose.
     * @param itemset the itemset
     * @throws IOException if error while writting to output file
     */
    private void output(int tempPosition, int utility) throws IOException {
        patternCount++;
            
        	// if user wants to save the results to memory
		if (writer == null) {
			// we copy the temporary buffer into a new int array
			int[] copy = new int[tempPosition+1];
			System.arraycopy(temp, 0, copy, 0, tempPosition+1);
			// we create the itemset using this array and add it to the list of itemsets
			// found until now
			highUtilityItemsets.addItemset(new Itemset(copy, utility),copy.length); 
		} else {
			// if user wants to save the results to file
			// create a stringuffer
			StringBuffer buffer = new StringBuffer();
			// append each item from the itemset to the stringbuffer, separated by spaces
			for (int i = 0; i <= tempPosition; i++) {
				buffer.append(temp[i]);
				if (i != tempPosition) {
					buffer.append(' ');
				}
			}
			// append the utility of the itemset
			buffer.append(" #UTIL: ");
			buffer.append(utility);
			
			// write the stringbuffer to file and create a new line
			// so that we are ready for writing the next itemset.
			writer.write(buffer.toString());
			writer.newLine();
		}
    }


 
 
    /**
     * Print statistics about the latest execution of the EFIM algorithm.
     */
	public void printStats() {

		System.out.println("========== EFAHM v97 - STATS ============");
		System.out.println(" minUtil = " + minUtil);
		System.out.println(" High utility itemsets count: " + patternCount);	
		System.out.println(" Total time ~: " + (endTimestamp - startTimestamp)
				+ " ms");
		// if in debug mode, we show more information
		if(DEBUG) {
			System.out.println(" Transaction merge count ~: " + mergeCount);	
			System.out.println(" Transaction read count ~: " + transactionReadingCount);	
			
			System.out.println(" Time intersections ~: " + timeIntersections
					+ " ms");	
			System.out.println(" Time database reduction ~: " + timeDatabaseReduction
					+ " ms");
			System.out.println(" Time promising items ~: " + timeIdentifyPromisingItems
					+ " ms");
			System.out.println(" Time binary search ~: " + timeBinarySearch
					+ " ms");
			System.out.println(" Time sort ~: " + timeSort	+ " ms");
		}
		System.out.println(" Max memory:" + MemoryLogger.getInstance().getMaxMemory());
		System.out.println(" Candidate count : "             + candidateCount);
		System.out.println("=====================================");
	}
}

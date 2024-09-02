//package ca.pfv.spmf.test;
package   ca.pfv.spmf.algorithms.frequentpatterns.efimPlus;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URL;

import ca.pfv.spmf.algorithms.frequentpatterns.efimPlus.AlgoEFIMPlus;


/**
 * Example of how to run the EFIM algorithm from the source code, and save the result to an output file.
 * @author Philippe Fournier-Viger, 2015
 */
public class MainTestEFIMPlus_saveToFile {

	public static void main(String [] arg) throws IOException{

		// the input and output file paths
		//String input = fileToPath("DB_Utility.txt");
		String input =  "f://Datasets//retail_utility_spmf.txt";
		String output = ".//output.txt";
		//String output = null;
		String output1 = ".//NewmushroomEFIM+.txt";
		BufferedWriter writer1 = new BufferedWriter(new FileWriter(output1));
//		for (int i=3;i<=10;i++)
	//		for (int j = 10000;j<=10000;j+=10000)		
			{
		// the minutil threshold
		//	int minutil = 30; 		
	
			// Run the EFIM algorithm
			AlgoEFIMPlus algo = new AlgoEFIMPlus();
			algo.runAlgorithm(7000,  input, output,false, Integer.MAX_VALUE, true,3);
		//	algo.runAlgorithm(minutil,  input, output, true, Integer.MAX_VALUE, true,3);
			// Print statistics
			algo.SaveResult(writer1, 10000, 3);
			algo.printStats();
			}
		writer1.close();
		// if true in next line it will find only closed itemsets, otherwise, all frequent itemsets
		//===================
//		Itemsets itemsets = algo.runAlgorithm(minutil,  input, null, activateMerging, maximumNumberOfTransactions, activateSubTreeUtilityPruning);
//		//==========================
//		itemsets.printItemsets();
	}
	
	public static String fileToPath(String filename) throws UnsupportedEncodingException{
		URL url = MainTestEFIMPlus_saveToFile.class.getResource(filename);
		 return java.net.URLDecoder.decode(url.getPath(),"UTF-8");
	}
}

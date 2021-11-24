
import pandas as pd
import numpy as np
def InitRDFClass():
    """Initial Ontology class"""
    
s = pd.Series(np.random.randn(5), index=["a", "b", "c", "d", "e"])
EL = pd.read_csv("D:\EL.csv", sep=";",parse_dates=['Start Time'],   )
print(EL.info())
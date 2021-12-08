
from owlready2 import *
import pm4py
from pm4py.objects.log.importer.xes import importer as xes_importer
log1 = 0 
onto = get_ontology("http://EL.org/onto.owl")
with onto:
    class log(Thing):
        pass
    class trace(Thing):
        pass
    class event(Thing):
        pass
    class attribute(Thing):
        key=""
        val=""
        def __init__(self,name,key,val):
            super().__init__(name)
            self.key = key
            self.val = val
    class global_attribute(Thing):
        pass
  

    l=0
    tr=0
    ev=0
    log1 = xes_importer.apply("D:\\test.xes")
    l= log("l_0")
    at_num=0
    for att in log1.attributes:
        at = attribute(name="attr_",key=att,val=log1.attributes[att])
        setattr(l,att , at)
    tr_count=0
    for trace in log1:
        tr = trace("tr_"+str(tr_count))
        for att in trace.attributes:
            at = attribute(name="attr_",key=att,val=trace.attributes[att])
            setattr(tr,att , at)
        for event in trace:
            for key in event:
                print(key)

from owlready2 import *
from rdflib import Graph
import pm4py

import pandas as pd

def get_csvfile(path):
    return pd.read_csv(path, sep=";",parse_dates=['Start Time'] )

log = pm4py.read_xes("D:\EL_Empty.csv")
onto = get_ontology("http://EL.org/onto.owl")
with onto:
 class el_trace(Thing):
    pass
 class el_Process(Thing):
    pass
 class el_event(Thing):
    pass
 class el_organization(Thing):
    pass
 class el_resource(Thing):
    pass
 class el_role(Thing):
    pass
 class el_activity(Thing):
    pass
###########################Object Properties ############################################
 class has_act(ObjectProperty):
    domain = [el_event]
    range  = [el_activity] 
 class has_Trace(ObjectProperty):
    domain = [el_Process]
    range  = [el_trace] 
 class has_event(ObjectProperty):
    domain = [el_trace]
    range  = [el_event]
 class has_res(ObjectProperty):
    domain = [el_event]
    range  = [el_resource]
###########################data Properties ############################################
 class act_name(DataProperty):
    domain = [el_activity]
    range=[str]
 class pr_ID(DataProperty):
    domain = [el_Process]
    range=[str]
 class tr_ID(DataProperty):
    domain = [el_trace]
    range=[str]
 class ev_starttime(DataProperty):
    domain =[el_event]
    range=[str]
 class ev_endtime(DataProperty):
    domain =[el_event]
    range=[str]
 class cost(DataProperty):
    domain = [el_event]
    range=[str]
#########################create Onto indivisuals#########################################
df = pd.read_csv("D:\EL_Empty.csv", sep=";" )
Event_Counter=0
trace_counter=0
process_counter=0
Activity_Counter=0
activity= {}
trace= {}  
el_pr = el_Process("pr_"+str(process_counter))
for index, row in df.iterrows():
     
    if Event_Counter>0 :
        el_tr=0
        el_act=0
 
        if row[0] in trace:
            el_tr = trace[row[0]]
        else:
            el_tr = el_trace("tr_"+str(trace_counter))
            trace_counter=trace_counter+1
            el_tr.tr_ID =[row[0]]
            el_tr.has_event=[]
            trace[row[0]] = el_tr
        el_pr.has_Trace.append(el_tr)
        if row[1] in activity:
            el_act = activity[row[1]]
        else:
            el_act = el_activity ("act_"+ str(Activity_Counter))
            #el_act = el_activity (row[1])
            Activity_Counter = Activity_Counter +1 
            el_act.act_name =[row[1]]            
            activity[row[1]] = el_act
         
        el_ev = el_event("ev_"+str(Event_Counter))
        current_event = el_ev
        el_ev.ev_starttime = [row[2]]
        el_ev.cost = [row[3]]
        el_ev.has_act = [el_act]
        el_tr.has_event.append( el_ev)
        
    Event_Counter = Event_Counter+1

print(f"Event Count : {Event_Counter}\n")
print(f"Activity Count : {Activity_Counter}\n")
print(f"Trace Count : {trace_counter}")
onto.save(file = "EL_RDF.owl", format = "rdfxml")


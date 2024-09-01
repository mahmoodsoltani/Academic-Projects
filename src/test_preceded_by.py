from owlready2 import *
from rdflib import Graph

import pandas as pd

def get_csvfile(path):
    return pd.read_csv(path, sep=";",parse_dates=['Start Time'] )

def add_indivisuals(row,i):
    el_act = el_activity ("act_"+ str(i))
    el_act.act_name =[row[1]]

onto = get_ontology("http://test.org/onto.owl")
with onto:
 class el_trace(Thing):
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
 class followed_by(ObjectProperty):
    domain = [el_event]
    range  = [el_event]
 class preceded_by(ObjectProperty):
    domain = [el_event]
    range  = [el_event]
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
 class tr_ID(DataProperty):
    domain = [el_trace]
    range=[str]
 class ev_timestamp(DataProperty):
    domain =[el_event]
    range=[str]
 class cost(DataProperty):
    domain = [el_event]
    range=[str]
#########################create Onto indivisuals#########################################
df = pd.read_csv("D:\EL_Empty.csv", sep=";" )
Event_Counter=0
trace_counter=0
Activity_Counter=0
activity= {}
trace= {}  
current_event=0
before_event=0
last = df.irow[0]
for i in range(1, df.shape[0]):
     
    if Event_Counter>0 :
        el_tr=0
        el_act=0
 
        if df.irow(i)[0] in trace:
            el_tr = trace[df.irow(i)[0]]
            before_event = current_event
        else:
            el_tr = el_trace("tr_"+str(trace_counter))
            trace_counter=trace_counter+1
            el_tr.tr_ID =[df.irow(i)[0]]
            el_tr.has_event=[]
            trace[df.irow(i)[0]] = el_tr
            before_event = 0
        if df.irow(i)[1] in activity:
            el_act = activity[df.irow(i)[1] ]
        else:
           # el_act = el_activity ("act_"+ str(Activity_Counter))
            el_act = el_activity (df.irow(i)[1] )
            Activity_Counter = Activity_Counter +1 
            el_act.act_name =[df.irow(i)[1] ]            
            activity[df.irow(i)[1] ] = el_act
         
        el_ev = el_event("ev_"+str(Event_Counter))
        current_event = el_ev
        el_ev.ev_timestamp = [df.irow(i)[2] ]
        el_ev.cost = [df.irow(i)[3]]
        el_ev.has_act = [el_act]
        if before_event!= 0 :
            el_ev.preceded_by = [before_event]
        el_tr.has_event.append( el_ev)
        
    Event_Counter = Event_Counter+1

print(f"Event Count : {Event_Counter}\n")
print(f"Activity Count : {Activity_Counter}\n")
print(f"Trace Count : {trace_counter}")
onto.save(file = "SampleRDF.owl", format = "rdfxml")


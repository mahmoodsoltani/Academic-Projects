from owlready2 import *
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
    domain = [el_event]
    range=[str]
 class cost(DataProperty):
    domain = [el_event]
    range=[str]
#########################create Onto indivisuals#########################################
df = pd.read_csv("D:\EL.csv", sep=";" )
i=0
j=1
activity= {}
trace= {}  
for index, row in df.iterrows():
     
    if i>0 :
        el_tr=0
        rl_act=0
        if row[1] in activity:
            rl_act = activity[row[1]]
        else:
            el_act = el_activity ("act_"+ str(i))
            el_act.act_name =[row[1]]
            activity[row[1]] = el_act
        if row[0] in trace:
            el_tr = trace[row[0]]
        else:
            el_tr = el_trace("tr_"+str(j))
            el_tr.tr_ID =[row[0]]
            el_tr.has_event=[]
            trace[row[0]] = el_tr
            j=j+1


        el_ev = el_event("ev_"+str(i))
        el_ev.ev_timestamp = [row[2]]
        el_ev.cost = [row[3]]
        
        el_ev.has_act = [el_act]
        el_tr.has_event.append( el_ev)
    i= i+1

def saveOntology():
    onto.save(file = "learnsystem.owl", format = "rdfxml")
      
saveOntology()



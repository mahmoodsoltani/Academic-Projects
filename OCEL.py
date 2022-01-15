import ocel
from owlready2 import *

def Create_TBOX():
    onto = get_ontology("http://EL.org/OCEL.owl")
    with onto:
        ############################CLASSES##########################
        class el_log(Thing):
            pass
        class el_event(Thing):
            pass
        class el_activity(Thing):
            pass
        class el_object(Thing):
            pass
        class el_objecttype(Thing):
            pass
        class el_resource(Thing):
            pass
        class el_attribute(Thing):
            pass
        ######################OBJ_PRO###########################
        class has_event(ObjectProperty):
            domain = [el_log]
            range  = [el_event]
        class has_att(ObjectProperty):
            domain = [el_event,el_objecttype]
            range  = [el_attribute]
        class has_object(ObjectProperty):
            domain = [el_log]
            range  = [el_object]
        class has_res(ObjectProperty):
            domain = [el_event]
            range  = [el_resource]
        class has_type(ObjectProperty):
            domain = [el_objecttype]
            range  = [el_object]
        class has_objecttype(ObjectProperty):
            domain = [el_event]
            range  = [el_objecttype]
        class has_activity(ObjectProperty):
            domain = [el_event]
            range  = [el_activity]
        #########################DATA_PRO###########################
        class ev_activity(DataProperty):
            domain = [el_event]
            range=[str]
        class act_name(DataProperty):
            domain = [el_activity]
            range=[str]
        class ev_timestamp(DataProperty):
            domain = [el_event]
            range=[str]
        class ev_id(DataProperty):
            domain = [el_event]
            range=[str]
        class res_name(DataProperty):
            domain = [el_resource]
            range=[str]
        class obj_name(DataProperty):
            domain = [el_object]
            range=[str]
        class objtype_name(DataProperty):
            domain = [el_objecttype]
            range=[str]
        class att_key(DataProperty):
            domain = [el_attribute]
            range=[str]
        class att_value(DataProperty):
            domain = [el_attribute]
            range=[str]
    return onto

def CreateOnto():
    All_Object = {}
    All_Objecttype = {}
    All_Activity = {}
    log = ocel.import_log("D:\\PHD\\py\\DataSet\\XML_OCEL_Small.xmlocel")
    onto = Create_TBOX()
    el_l = onto.el_log("L1")
    ######################### Create Object instancess ################
    for obj in ocel.get_object_types(log):
        obj_instance = onto.el_object()
        obj_instance.name = obj
        All_Object[obj] = obj_instance
    ######################### Create Objecttype instancess ################
    obj_ins = ocel.get_objects(log)
    for obj in obj_ins:
        objtype_instance = onto.el_objecttype()
        objtype_instance.name = obj
        objtype_instance.has_type.append(All_Object[obj_ins[obj]["ocel:type"]])
        All_Objecttype[obj] = objtype_instance
######################### Create Objecttype attributes ################
        ovmap = obj_ins[obj]["ocel:ovmap"]
        for v in ovmap:
            el_att = onto.el_attribute()
            el_att.att_key = [v]
            el_att.att_value = [obj_ins[obj]["ocel:ovmap"][v]]
            objtype_instance.has_att.append(el_att)
######################### Create event instancess ################
    events = ocel.get_events(log)
    for ev in events:
        el_instance = onto.el_event()
        # check if activity seen before if not create object and add to dictionary
        if not events[ev]["ocel:activity"] in All_Activity:
            el_act = onto.el_activity()
            el_act.act_name = [events[ev]["ocel:activity"]]
            All_Activity[events[ev]["ocel:activity"]] = el_act
        el_instance.has_activity.append(All_Activity[events[ev]["ocel:activity"]])
        el_instance.ev_timestamp = [events[ev]["ocel:timestamp"]]
######################### add event objects ################
        omap = events[ev]["ocel:omap"]
        for o in omap:
            el_instance.has_objecttype.append(All_Objecttype[o])
######################### add object attribute(s) ################
        vmap = events[ev]["ocel:vmap"]
        for v in vmap:
            el_att = onto.el_attribute()
            el_att.att_key = [v]
            el_att.att_value = [events[ev]["ocel:vmap"][v]]
            el_instance.has_att.append(el_att)

    onto.save(file = "ocel_small.owl", format = "rdfxml")

CreateOnto()
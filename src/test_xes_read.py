
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
    class extension(Thing):
        pass
    class attribute(Thing):
        pass
    class global_attribute(attribute):
        pass
    class ext_uri(DataProperty, FunctionalProperty):
        domain = [extension]
        range = [str]
    class ext_pre(DataProperty, FunctionalProperty):
        domain = [extension]
        range = [str]
    class att_scope(global_attribute>>str):
        pass
    class att_key(DataProperty):
        domain=[attribute]
        range =[str]
    class att_value(DataProperty):
        domain=[attribute]
        range =[str]
    class has_trace(log>>trace):
        pass
    class has_event(trace>>event):
        pass
    class has_attr(Thing>>attribute):
        pass
    class has_ext(attribute>>extension):
        pass
    l=0
    tr=0
    ev=0
    log1 = xes_importer.apply("D:\\bigXES.xes")
    l= log("l_0")
    at_num=0
    for ex in log1.extensions:
        ext = extension(ex)
        ext.ext_pre = log1.extensions[ex]['prefix']
        ext.ext_uri = log1.extensions[ex]['uri']
    for att in log1.attributes:
        if type(log1.attributes[att]) is dict:
            continue
        # at = attribute(name="attr_",key=att,val=log1.attributes[att])
        # setattr(l,att , at)
        _att = attribute(att)
        _att.att_key.append(att)
        _att.att_value.append(log1.attributes[att])
        l.has_attr.append(_att)
    for att in log1.attributes:
        if type(log1.attributes[att]) is dict:
            continue
        # at = attribute(name="attr_",key=att,val=log1.attributes[att])
        # setattr(l,att , at)
        _att = attribute(att)
        _att.att_key.append(att)
        _att.att_value.append(log1.attributes[att])
        l.has_attr.append(_att)
    tr_count=0
    ev_count=0
    for traces in log1:
        tr = trace("tr_"+str(tr_count))
        tr_count=tr_count+1
        for att in traces.attributes:
            if type(traces.attributes[att]) is dict:
                continue
            # at = attribute(name="attr_",key=att,val=traces.attributes[att])
            # setattr(tr,att , at)
            # tr.has_attr.append(at)
            _att = attribute()
            for _extension in extension.instances():
                if _extension.ext_pre+':' in att:
                    _att.has_ext.append(_extension)
                    break
            _att.att_value.append(traces.attributes[att])
            _att.att_key.append(att)
            tr.has_attr.append(_att)
        l.has_trace.append(tr)
        for events in traces:
            ev = event("ev_"+str(ev_count))
            ev_count=ev_count+1

            for att in events:
                find_att=1
                if type(events[att]) is dict:
                    continue
                for _findAtt in attribute.instances():
                    if _findAtt.att_key[0] == att and _findAtt.att_value[0] == events[att]:
                        ev.has_attr.append(_findAtt)
                        find_att=0
                        break
                if find_att:
                    _att = attribute("")
                    for _extension in extension.instances():
                        if _extension.ext_pre+':' in att:
                            _att.has_ext.append(_extension)
                            break
                    _att.att_value.append(events[att])
                    _att.att_key.append(att)
                    ev.has_attr.append(_att)
            tr.has_event.append(ev)
onto.save(file = "new_RDF.owl", format = "rdfxml")
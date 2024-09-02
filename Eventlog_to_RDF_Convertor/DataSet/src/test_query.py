from owlready2 import *
from rdflib import Graph

g = Graph()
graph = default_world.as_rdflib_graph()
g.parse("learnsystem.owl")
#g.serialize(destination="tbl.ttl")
q = """
    SELECT ?tr
    WHERE {
        ?tr :has_event ?ev .
        ?ev :has_act :act_13 .
    }
"""

# Apply the query to the graph and iterate through results
for r in g.query(q):
    print(r["tr"])
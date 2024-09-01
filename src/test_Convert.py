from owlready2 import *
from rdflib import Graph

g = Graph()
graph = default_world.as_rdflib_graph()
g.parse("learnsystem.owl")
g.serialize(destination="tbl.ttl")
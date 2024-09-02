import rdflib
from owlready2 import *
from rdflib.extras.external_graph_libs import rdflib_to_networkx_multidigraph
import networkx as nx
import matplotlib.pyplot as plt
import io
import pydotplus
from IPython.display import display, Image
from rdflib.tools.rdf2dot import rdf2dot

g = rdflib.Graph()
graph = default_world.as_rdflib_graph()
result=g.parse("SampleRdf.owl")



def visualize(g):
    stream = io.StringIO()
    rdf2dot(g, stream, opts = {display})
    dg = pydotplus.graph_from_dot_data(stream.getvalue())
    dg.write_png('test.png')
    png = dg.create_png()
    display(Image(png))

visualize(g)
# G = rdflib_to_networkx_multidigraph(result)

# # Plot Networkx instance of RDF Graph
# pos = nx.spring_layout(G, scale=5)
# edge_labels = nx.get_edge_attributes(G, 'b')
# nx.draw_networkx_edge_labels(G, pos, edge_labels=edge_labels)
# nx.draw(G, with_labels=True)

#if not in interactive mode for 

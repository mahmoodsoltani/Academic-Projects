import bpmn_python.bpmn_diagram_visualizer as visualizer
import bpmn_python.bpmn_diagram_rep as diagram
import xml.etree.ElementTree as ET
tree = ET.parse('D:\\sale.bpmn')
root = tree.getroot()
for neighbor in root.iter('bpmn:definitions'):
    print(neighbor.attrib)
# bpmn_graph = diagram.BpmnDiagramGraph()
# bpmn_graph.load_diagram_from_xml_file("D:\\Sale.bpmn")
# visualizer.visualize_diagram(bpmn_graph)
print("test")
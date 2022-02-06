import yaml
from dataclasses import dataclass
# class WorkItem(yaml.YAMLObject):
#     yaml_tag = u'!WorkItem'
#     def __init__(self, title, start_data, end_data, branch,actor,approved,status,content):
#         self.title = title
#         self.start_data = start_data
#         self.end_data = end_data
#         self.branch = branch
#         self.actor = actor
#         self.approved = approved
#         self.status = status
#         self.content = content

@dataclass
class WorkItem(yaml.YAMLObject):
    title:str
    start_data:str
    end_data:str
    branch:str
    actor:str
    approved:str
    status:str
    content:str
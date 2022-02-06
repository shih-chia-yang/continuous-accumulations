import codecs
import time
import yaml
import sys
import ViewModels


    


# def noop(self, *args, **kw):
#     pass

# yaml.emitter.Emitter.process_tag = noop
# yaml.dump([
#     ViewModels.WorkItem('自動產生工作事項','20220125','20220126','issue/ci/work_item','stone','','todo','讀取會議記錄後自動產生工作事項'),
    # ViewModels.Monster(name='Sméagol', hp=400, ac=14, attacks=['TOUCH','EAT-GOLD']),
# ], sys.stdout, allow_unicode=True)
item=ViewModels.WorkItem('自動產生工作事項','20220125','20220126','issue/ci/work_item','stone','','todo','讀取會議記錄後自動產生工作事項')
d = {'name':'Amy', 'age':'20', 'languages':['English', 'French']}
workid=f'{time.strftime("%Y%m%d")}001'
with open(f'{workid}.yaml','w',encoding="utf-8") as f:
    # yaml.(WorkItem)
    yaml.dump([item,item], f,default_flow_style=False,encoding='utf-8',allow_unicode=True)
    print(item)
from ..Models import DBSession, Base
from pyramid.view import view_config
import json
from pyramid.security import NO_PERMISSION_REQUIRED
from sqlalchemy import select
from ..utils import * 


@view_config(route_name='log',permission=NO_PERMISSION_REQUIRED,request_method='GET',renderer='json')

def get_logs(request): 
    table = Base.metadata.tables['TLOG_MESSAGES']
    data = request.params.mixed()

    searchInfo = {}
    searchInfo['criteria'] = []
    if 'criteria' in data: 
        data['criteria'] = json.loads(data['criteria'])
        if data['criteria'] != {} :
            searchInfo['criteria'] = [obj for obj in data['criteria'] if obj['Value'] != str(-1) ]

    searchInfo['order_by'] = json.loads(data['order_by'])
    searchInfo['offset'] = json.loads(data['offset'])
    searchInfo['per_page'] = json.loads(data['per_page'])

    print(searchInfo)

    searchGene = Generator(table,DBSession)
    result = searchGene.search(searchInfo['criteria'],searchInfo['offset'],searchInfo['per_page'],searchInfo['order_by'])

    return result

@view_config(route_name= 'log/id',permission=NO_PERMISSION_REQUIRED, renderer='json', request_method = 'GET')
def getLog(request):
    id = request.matchdict['id']
    print(id)
    table = Base.metadata.tables['TLOG_MESSAGES']
    query = select(table.c).where(table.c['ID']==id)
    curLog = DBSession.execute(query).fetchone()
    return dict(curLog)


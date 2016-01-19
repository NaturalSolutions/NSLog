from ..Models import DBSession, Base
from pyramid.view import view_config
import json
from pyramid.security import NO_PERMISSION_REQUIRED
from sqlalchemy import select


@view_config(route_name='log',permission=NO_PERMISSION_REQUIRED,request_method='GET',renderer='json')

def get_logs(request): 
    table = Base.metadata.tables['TLOG_MESSAGES']
    data = request.params.mixed()
    print(data)

    searchInfo = {}
    searchInfo['criteria'] = []
    if 'criteria' in data: 
        data['criteria'] = json.loads(data['criteria'])
        if data['criteria'] != {} :
            searchInfo['criteria'] = [obj for obj in data['criteria'] if obj['Value'] != str(-1) ]

    print(searchInfo)
    query = select(table.c)
    results = DBSession.execute(query).fetchall()
    response = []



    for row in results :
        response.append(dict(row))

    # response = [dict(row) for row in results]
    response = [{'total_entries': len(results)},response]
    return response

@view_config(route_name= 'log/id',permission=NO_PERMISSION_REQUIRED, renderer='json', request_method = 'GET')
def getLog(request):
    id = request.matchdict['id']
    print(id)
    table = Base.metadata.tables['TLOG_MESSAGES']
    query = select(table.c).where(table.c['ID']==id)
    curLog = DBSession.execute(query).fetchone()
    return dict(curLog)


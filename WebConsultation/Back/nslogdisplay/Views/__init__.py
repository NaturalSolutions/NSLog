### test if the match url is integer
from pyramid.request import Request
from pyramid.request import Response

def request_factory(environ):
    request = Request(environ)
    if request.is_xhr:
        request.response = Response()
        request.response.headerlist = []
        request.response.headerlist.extend(
            (
                ('Access-Control-Allow-Origin', '*')
            )
        )
    return request

def add_cors_headers_response_callback(event):
    def cors_headers(request, response):
        response.headers.update({
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'POST,GET,DELETE,PUT,OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept, Authorization',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Max-Age': '1728000',
        })
    event.request.add_response_callback(cors_headers)


def integers(*segment_names):
    def predicate(info, request):
        match = info['match']
        for segment_name in segment_names:
            try:
                print (segment_names)
                match[segment_name] = int(match[segment_name])
                if int(match[segment_name]) == 0 :
                    print(' ****** ACTIONS FORMS ******')
                    return False
            except (TypeError, ValueError):
                return False
        return True
    return predicate

def add_routes(config):
    ##### Security routes #####
    config.add_route('log', 'logdisplay-core/log')
    config.add_route('log/id', 'logdisplay-core/log/{id}',custom_predicates = (integers('id'),))
    # config.add_route('security/logout', 'portal-core/security/logout')
    # config.add_route('security/has_access', 'portal-core/security/has_access')

    #### User #####
    # config.add_route('core/user', 'portal-core/user')
    # config.add_route('core/currentUser', 'portal-core/currentUser')
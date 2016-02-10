# from sqlalchemy import (
    # Column,
    # DateTime,
    # Index,
    # Integer,
    # Sequence,
    # String,
    # func,
    # Boolean
    # )

# from sqlalchemy.ext.hybrid import hybrid_property
# from ..Models import Base, dbConfig

# db_dialect = dbConfig['dialect']

# class TLOG_MESSAGES(Base):
    # __tablename__ = 'TLOG_MESSAGES'
    # ID = Column( Integer, primary_key=True)
    # JCRE = Column(  DateTime, nullable=False)
    # LOG_LEVEL = Column( 'TUse_FirstName', String(50), nullable=False)
    # ORIGIN = Column( 'TUse_CreationDate', DateTime, nullable=False,server_default=func.now())
    # SCOPE = Column( 'TUse_Login', String(255), nullable=False)
    # LOGUSER = Column( 'TUse_Password', String(50), nullable=False)
    # DOMAINE = Column( 'TUse_Language', String(5))
    # MESSAGE_NUMBER = Column( 'TUse_ModificationDate', DateTime, nullable=False,server_default=func.now())
    # LOG_MESSAGE = Column( 'TUse_HasAccess', Boolean)
    # OTHERSINFOS = Column( 'TUse_Photo', String(255))

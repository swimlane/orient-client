﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orient.Client.Protocol.Serializers;

namespace Orient.Client.Protocol.Operations
{
    internal class DBSize : BaseOperation
    {
        public DBSize(ODatabase database)
            : base(database)
        {
            _operationType = OperationType.DB_SIZE;
        }
        public override Request Request(Request request)
        {
            base.Request(request);
            return request;
        }

        public override ODocument Response(Response response)
        {
            ODocument document = new ODocument();
            if (response == null)
            {
                return document;
            }

            var reader = response.Reader;
            if (response.Connection.ProtocolVersion > 26 && response.Connection.UseTokenBasedSession)
                ReadToken(reader); 
            
            var size = reader.ReadInt64EndianAware();
            document.SetField<long>("size", size);

            return document;
        }
    }
}

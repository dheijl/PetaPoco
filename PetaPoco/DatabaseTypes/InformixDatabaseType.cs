// PetaPoco - A Tiny ORMish thing for your POCO's.
// Copyright © 2011-2012 Topten Software.  All Rights Reserved.

using System;
using PetaPoco.Internal;


namespace PetaPoco.DatabaseTypes
{

    class InformixDatabaseType: DatabaseType
    {
        public override string GetParameterPrefix(string ConnectionString) {
            return "?";
        }

        public override string EscapeSqlIdentifier(string str) {
            return str;
        }

        public override string GetExistsSql() {
            return @"SELECT 1 AS found FROM systables where tabid = 1 and EXISTS
                         (SELECT * FROM {0} WHERE {1})";
        }

        public override object ExecuteInsert(Database db, System.Data.IDbCommand cmd, string PrimaryKeyName) {
            db.ExecuteScalarHelper(cmd);
            cmd.CommandText = "SELECT DBINFO('sqlca.sqlerrd1') from systables where tabid = 1";
            return db.ExecuteScalarHelper(cmd);
        }


    }
}

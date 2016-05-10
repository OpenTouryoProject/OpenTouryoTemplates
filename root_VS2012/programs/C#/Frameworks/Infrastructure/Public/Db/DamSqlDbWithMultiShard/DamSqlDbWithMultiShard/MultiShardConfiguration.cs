//**********************************************************************************
//* Copyright (C) 2007,2016 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：MultiShardConfiguration
//* クラス日本語名  ：データアクセス・プロバイダ＝DB2.NETのデータアクセス制御クラス
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//* 
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2016/04/25  Supragyan         Created  MultiShardConfiguration class to implement 
//*                                common properties,methods to get the shards 
//**********************************************************************************

//system
using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Microsoft
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
//touryo
using Touryo.Infrastructure.Public.Util;
using System.Data.SqlClient;

namespace DamSqlDbWithMultiShard
{
    public static class MultiShardConfiguration
    {
        /// <summary>shardMapName</summary>
        private static string _shardMapName;

        /// <summary>ShardMapManagerDatabaseName</summary>
        private static string _shardMapManagerDatabaseName;

        /// <summary>Shards</summary>
        private static IEnumerable<Shard> _shards;

        /// <summary>
        /// Gets the server name from the App.config file for shards to be created on.
        /// </summary>
        private static string _serverName
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerName"];
            }
        }

        /// <summary>
        /// Gets the server name from the App.config file for shards to be created on.
        /// </summary>
        private static string _databaseEdition
        {
            get
            {
                return ConfigurationManager.AppSettings["DatabaseEdition"];
            }
        }
        
        /// <summary>customerId</summary>
        public static int customerId;

        /// <summary>shardMap</summary>
        public static string connStr = GetConfigParameter.GetConnectionString("ConnectionString_AzureSQL");

        /// <summary>SQL master database name.</summary>
        public static string MasterDatabaseName;

        /// <summary>
        /// Gets all Shards.
        /// </summary>
        public static IEnumerable<Shard> Shards
        {
            get
            {
                return _shards;
            }
            set
            {
                _shards = value;
            }
        }

        /// <summary>
        /// Gets the name for the Shard Map that contains metadata for all the shards and the mappings to those shards.
        /// </summary>
        public static string ShardMapName
        {
            get
            {
                return _shardMapName;
            }
            set
            {
                _shardMapName = value;
            }
        }

        /// <summary>
        /// Gets the server name for the Shard Map Manager database, which contains the shard maps.
        /// </summary>
        public static string ShardMapManagerServerName
        {
            get
            {
                return _serverName;
            }
        }

        /// <summary>
        /// Gets the database name for the Shard Map Manager database, which contains the shard maps.
        /// </summary>
        public static string ShardMapManagerDatabaseName
        {
            get
            {
                return _shardMapManagerDatabaseName;
            }
            set
            {
                _shardMapManagerDatabaseName = value;
            }
        }

        /// <summary>
        /// Gets the edition to use for Shards and Shard Map Manager Database if the server is an Azure SQL DB server. 
        /// If the server is a regular SQL Server then this is ignored.
        /// </summary>
        public static string DatabaseEdition
        {
            get
            {
                return _databaseEdition;
            }
        }

        /// <summary>
        /// objShardMapManager
        /// </summary>
        public static ShardMapManager objShardMapManager;

        #region Shard map helper methods

        /// <summary>
        /// Gets the shard map, if it exists. If it doesn't exist, writes out the reason and returns null.
        /// </summary>
        public static RangeShardMap<int> TryGetShardMap()
        {
            if (objShardMapManager == null)
            {
                return null;
            }

            RangeShardMap<int> shardMap;
            bool mapExists = objShardMapManager.TryGetRangeShardMap(ShardMapName, out shardMap);

            if (!mapExists)
            {
                return null;
            }

            return shardMap;
        }

        #endregion

        #region connectionstring methods

        /// <summary>
        /// Returns a connection string that can be used to connect to the specified server and database.
        /// </summary>
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder sbConnStr = new SqlConnectionStringBuilder(connStr);
            sbConnStr.DataSource = ShardMapManagerServerName;
            sbConnStr.InitialCatalog = ShardMapManagerDatabaseName;
            return sbConnStr.ToString();
        }

        /// <summary>
        /// Returns a connection string that can be used to connect to the specified server and database.
        /// </summary>
        public static string GetConnectionStringByMasterDatabase()
        {
            SqlConnectionStringBuilder sbConnStr = new SqlConnectionStringBuilder(connStr);
            sbConnStr.DataSource = ShardMapManagerServerName;
            sbConnStr.InitialCatalog = MasterDatabaseName;
            return sbConnStr.ToString();
        }

        /// <summary>
        /// Returns a connection string that can be used to connect to the specified shard.
        /// </summary>
        /// <param name="connstring"></param>
        /// <returns></returns>
        public static SqlConnection GetDataDependentRoutingConnectionString(string connstring)
        {
           return MultiShardConfiguration.TryGetShardMap().OpenConnectionForKey(MultiShardConfiguration.customerId, connstring);
        }

        /// <summary>
        /// Returns a connection string that can be used to connect to the specified server and database.
        /// </summary>
        public static string GetConnectionStringBySelectedDatabase(string database)
        {
            SqlConnectionStringBuilder sbConnStr = new SqlConnectionStringBuilder(connStr);
            sbConnStr.DataSource = ShardMapManagerServerName;
            sbConnStr.InitialCatalog = database;
            return sbConnStr.ToString();
        }

        #endregion
    }
}

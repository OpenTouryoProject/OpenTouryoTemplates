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

        /// <summary>
        /// Tries to get the ShardMapManager that is stored in the specified database.
        /// </summary>
        public static ShardMapManager TryGetShardMapManager()
        {
            string connStr = GetConfigParameter.GetConnectionString("ConnectionString_AzureSQL");

            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder(connStr);

            stringBuilder.DataSource = ShardMapManagerServerName;
            stringBuilder.InitialCatalog = ShardMapManagerDatabaseName;

            ShardMapManager shardMapManager;
            bool smmExists = ShardMapManagerFactory.TryGetSqlShardMapManager(stringBuilder.ToString(), ShardMapManagerLoadPolicy.Lazy, out shardMapManager);

            return shardMapManager;
        }
    }
}

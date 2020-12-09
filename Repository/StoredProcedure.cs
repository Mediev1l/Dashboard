using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyBoard.DataAccess.Data;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class StoredProcedure : IStoredProcedure
    {
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public StoredProcedure(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                sql.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                return (T)Convert.ChangeType(sql.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                var value =  sql.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

                return (T)Convert.ChangeType(value.FirstOrDefault(), typeof(T));
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                return sql.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                var result = SqlMapper.QueryMultiple(sql, procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<T1>().ToList();
                var item2 = result.Read<T2>().ToList();

                if(item1 != null && item2 != null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
                }

            }

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

    }
}

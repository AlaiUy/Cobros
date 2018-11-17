using Aguiñagalde.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Aguiñagalde.DAL
{
    public class ArticulosMapper : DataAccess, IMapperAriculos
    {
        private IDbConnection _Connection;

        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new SqlConnection(GlobalConnectionString);
                }
                return _Connection;
            }

        }

        

        public void Descatalogar(string xCodigo)
        {
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@CODIGO", xCodigo));
            DbCommand Command = new SqlCommand("UPDATE ARTICULOS SET DESCATALOGADO  = 'T' WHERE (REFPROVEEDOR = @CODIGO)", (SqlConnection)Connection);
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
                ExecuteNonQuery(Command, P);
                CerrarConexion(Connection);
            }
        }

        public override int ExecuteNonQuery(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public override int ExecuteNonQuery(DbCommand cmd, List<IDataParameter> Lts)
        {
            {
                foreach (IDataParameter P in Lts)
                {
                    cmd.Parameters.Add(P);
                }

                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception E)
                {
                    CerrarConexion(cmd.Connection);
                    throw E;
                }
            }
        }

        public override IDataReader ExecuteReader(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public override IDataReader ExecuteReader(DbCommand cmd, List<IDataParameter> Lts)
        {
            foreach (IDataParameter P in Lts)
            {
                cmd.Parameters.Add(P);
            }
            try
            {
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }

        }

        public override object ExecuteScalar(DbCommand cmd, List<IDataParameter> Lts)
        {

            foreach (IDataParameter P in Lts)
            {
                cmd.Parameters.Add(P);
            }
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }


        }

        public override object ExecuteScalar(DbCommand cmd)
        {
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                CerrarConexion(cmd.Connection);
                throw e;
            }
        }

        public void Add(object o)
        {
            throw new NotImplementedException();
        }

        public void Update(object o)
        {
            throw new NotImplementedException();
        }

        public void Delete(object o)
        {
            throw new NotImplementedException();
        }
    }
}

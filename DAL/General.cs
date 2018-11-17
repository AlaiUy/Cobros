using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aguiñagalde.Entidades;
using System.Data.Common;
using System.Data.SqlClient;

namespace Aguiñagalde.DAL
{
    public class General : DataAccess
    {
        private IDbConnection _Connection;

        // private IDbConnection _ConnectionGeneral;

        private List<Moneda> _ListaMonedas;

        private static General _Instance = null;

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

        private General()
        {
            _ListaMonedas = new List<Moneda>();
            _ListaMonedas = getMonedas();
        }

        public static General getInstance()
        {
            if (_Instance == null)
                _Instance = new General();

            return _Instance;

        }

        public List<Moneda> getMonedas()
        {
            if (_ListaMonedas != null && _ListaMonedas.Count >0)
                return _ListaMonedas;
            else
            {
                DbCommand Command = new SqlCommand("SELECT top 2 CODMONEDA AS ID,DESCRIPCION AS NOMBRE, INICIALES,MORA FROM MONEDAS where codmoneda between 1 and 2", (SqlConnection)Connection);
                System.Data.IDataReader rd = null;
                List<Moneda> FormasPago = new List<Moneda>();
                Moneda Entity = null;
                try
                {
                    if (_Connection.State == System.Data.ConnectionState.Closed)
                        _Connection.Open();
                    rd = ExecuteReader(Command);
                    while (rd.Read())
                    {
                        Entity = (Moneda)getMonedaFromReader(rd);
                        if (Entity.Descripcion.Length > 0)
                            FormasPago.Add(Entity);
                    }
                    rd.Close();
                    return FormasPago;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    CerrarConexion(_Connection);
                }
            }
        }

        internal Moneda getMonedaByID(int Moneda)
        {
            foreach (Moneda M in _ListaMonedas)
            {
                if (M.Codmoneda == Moneda)
                    return (Moneda)M;
            }
            List<IDataParameter> P = new List<IDataParameter>();
            P.Add(new SqlParameter("@ID", Moneda));
            DbCommand Command = new SqlCommand("SELECT TOP 1 CODMONEDA AS ID,DESCRIPCION AS NOMBRE, INICIALES,MORA FROM MONEDAS where codmoneda = @ID", (SqlConnection)Connection);
            System.Data.IDataReader rd = null;
            Moneda Entity = null;
            try
            {
                if (_Connection.State == System.Data.ConnectionState.Closed)
                    _Connection.Open();
                rd = ExecuteReader(Command, P);
                if (rd.Read())
                {
                    Entity = (Moneda)getMonedaFromReader(rd);

                }
                rd.Close();
                return Entity;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CerrarConexion(_Connection);
            }
        }

        private Moneda getMonedaFromReader(IDataReader Reader)
        {
            try
            {
                Moneda Entity = new Moneda((int)Reader["ID"], (string)Reader["Nombre"]);
                Entity.Iniciales = (string)(Reader["INICIALES"] is DBNull ? 0 : Reader["INICIALES"]);
                Entity.Mora = (decimal)Convert.ToDouble(((Reader["MORA"] is DBNull ? 0 : Reader["MORA"])));
                return Entity;
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}

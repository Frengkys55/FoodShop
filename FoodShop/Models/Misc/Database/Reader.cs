using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Reflection;

namespace FoodShop.Models.Misc.Database
{
    /// <summary>
    /// Generic function to read from database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRead<T>
    {

        /// <summary>
        /// Read a data from database with object T as a reference
        /// </summary>
        /// <param name="query">Your database query</param>
        /// <param name="connectionString">Your database connection string</param>
        /// <param name="parameters">Any additional parameter</param>
        /// <returns></returns>
        public async Task<ICollection<T>> Read(string query, string connectionString, Collection<KeyValuePair<string, string>> parameters = null)
        {
            Collection<T> data = new Collection<T>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(query, con);

            if (parameters != null)
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Clear();
                foreach (var parameter in parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                }
            }

            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Get field count
                            int fieldCount = reader.FieldCount;

                            object obj = Activator.CreateInstance(typeof(T));   // Create new instance of T
                            Type type = obj.GetType();                          // Get the type of T
                            PropertyInfo[] info = type.GetProperties();         // Get all public properties of T

                            // Set value to all properties
                            for (int i = 0; i < info.Length; i++)
                            {
                                string fieldName = info[i].Name;
                                object? value = null;
                                try
                                {
                                    value = reader[fieldName];
                                }
                                catch
                                {
                                    value = null;
                                }
                                // Specialized convertion
                                string propertyTypeName = info[i].PropertyType.Name;
                                if (propertyTypeName == "Guid")
                                {
                                    info[i].SetValue(obj, Guid.Parse(value as string));
                                }
                                else
                                {
                                    try
                                    {
                                        info[i].SetValue(obj, value);
                                    }
                                    catch
                                    {
                                        info[i].SetValue(obj, null);
                                    }
                                }
                            }
                            data.Add((T)obj);
                        }
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
                await con.DisposeAsync();
                await sqlCommand.DisposeAsync();
                await reader.DisposeAsync();
            }

            return data;
        }
    }
}

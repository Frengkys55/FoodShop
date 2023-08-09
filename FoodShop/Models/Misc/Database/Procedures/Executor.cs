using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace FoodShop.Models.Misc.Database.Procedures
{
    public class Executor
    {
        string _connectionString { get; set; }

        public Executor(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Execute SQL Procedure
        /// </summary>
        /// <typeparam name="T">Your object input type</typeparam>
        /// <typeparam name="TReturn">Your object return type (will always return a collection)</typeparam>
        /// <param name="procedureName">Name of the procedure</param>
        /// <param name="data">Your T object information</param>
        /// <param name="propertyToIgnore">List of properties to ignore from your data</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<TReturn>> Execute<T, TReturn>(string procedureName, T data = default, List<string> propertyToIgnore = default, List<KeyValuePair<string, dynamic>> AdditionalData = null)
        {
            if (procedureName == null)
                throw new ArgumentNullException(nameof(procedureName));


            Collection<TReturn> returnObject = new Collection<TReturn>();

            SqlConnection con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            SqlCommand com = new SqlCommand(procedureName, con);

            com.CommandType = System.Data.CommandType.StoredProcedure;

            if (data != null)
            {
                // Create new instance of T and read all public properties
                Type type = data.GetType();
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    if (propertyToIgnore != null && propertyToIgnore.Contains(property.Name))
                        continue;

                    string name = property.Name;
                    object value = null;
                    try
                    {
                        value = property.GetValue(data);
                    }
                    catch (Exception)
                    {
                        value = DBNull.Value;
                    }

                    com.Parameters.Add(new SqlParameter("@" + name, value));
                }
            }

            if (AdditionalData != null)
            {
                foreach (var item in AdditionalData)
                {
                    com.Parameters.Add(new SqlParameter("@" + item.Key, item.Value));
                }
            }

            try
            {
                if (typeof(TReturn).Name.ToLower() == "void")
                {
                    int result = await com.ExecuteNonQueryAsync();
                }
                else
                {
                    var result = await com.ExecuteReaderAsync();
                    if (result != null)
                    {
                        while (result.Read())
                        {
                            object returnObj = Activator.CreateInstance(typeof(TReturn));
                            Type returnObjType = returnObj.GetType();
                            var properties = returnObjType.GetProperties();

                            for (int i = 0; i < properties.Length; i++)
                            {
                                string fieldName = properties[i].Name;
                                object? value = null;
                                try
                                {
                                    value = result[fieldName];
                                }
                                catch
                                {
                                    value = null;
                                }
                                // Specialized convertion
                                string propertyTypeName = properties[i].PropertyType.Name;
                                if (propertyTypeName == "Guid")
                                {
                                    properties[i].SetValue(returnObj, Guid.Parse(value as string));
                                }
                                else
                                {
                                    try
                                    {
                                        properties[i].SetValue(returnObj, value);
                                    }
                                    catch
                                    {
                                        properties[i].SetValue(returnObj, null);
                                    }
                                }
                            }
                            returnObject.Add((TReturn)returnObj);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await com.DisposeAsync();
                await con.CloseAsync();
                await con.DisposeAsync();
            }

            return returnObject;
        }
    }
}

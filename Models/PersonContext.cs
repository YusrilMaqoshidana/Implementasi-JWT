﻿using Microsoft.AspNetCore.Mvc;
using JWTAuth.Helpers;
using Npgsql;

namespace JWTAuth.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;
        public PersonContext(string pConstr)
        {
            __constr = pConstr;
        }
        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_person, nama, alamat, email FROM users.person;");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        public void PostPerson(Person person)
        {
            string query = string.Format(@"INSERT INTO users.person (id_person, nama, alamat, email) VALUES ('{0}', '{1}', '{2}', '{3}');", person.id_person, person.nama, person.alamat, person.email);
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }

        public void PutPerson(int id, Person person)
        {
            string query = string.Format(@"UPDATE users.person SET nama = '{0}', alamat = '{1}', email = '{2}' WHERE id_person = '{3}';", person.nama, person.alamat, person.email, id);
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }

        public void DeletePerson(int id)
        {
            string query = string.Format(@"DELETE FROM users.person WHERE id_person = {0}", id);
            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }
    }
}

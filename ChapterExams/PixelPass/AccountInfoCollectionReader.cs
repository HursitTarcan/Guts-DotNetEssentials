using System;
using System.IO;
using System.Windows;

namespace PixelPass
{
    public class AccountInfoCollectionReader
    {
        public static IAccountInfoCollection Read(string filename)
        {
            IAccountInfoCollection accountInfoCollection = new AccountInfoCollection();
            StreamReader reader = new StreamReader(filename);
            try
            {
                string line = reader.ReadLine();
                if (!line.StartsWith("Name:"))
                {
                    throw new ParseException($"Read from a file not starting with 'Name:' should throw ParseException. Look for {filename}");
                }
                else
                {
                    line = line.Replace("Name: ", ""); 
                }

                accountInfoCollection.Name = line;
                line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');
                    string title = parts[0];
                    string userName = parts[1];
                    string password = parts[2];
                    string notes = parts[3];
                    int[] dateInt = Array.ConvertAll(parts[4].Split('/'), int.Parse);
                    DateTime expirationDate = new DateTime(dateInt[2], dateInt[1], dateInt[0]);
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.Title = title;
                    accountInfo.Username = userName;
                    accountInfo.Password = password;
                    accountInfo.Notes = notes;
                    accountInfo.Expiration = expirationDate;
                    accountInfoCollection.AccountInfos.Add(accountInfo);
                    line = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return accountInfoCollection;
        }
    }
}

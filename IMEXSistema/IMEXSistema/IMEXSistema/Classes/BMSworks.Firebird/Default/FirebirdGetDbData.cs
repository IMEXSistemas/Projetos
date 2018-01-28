using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;


namespace BMSworks.Firebird
{
    public class FirebirdGetDbData
    {
        public bool ConvertDBValueToBoolean( FbDataReader reader, int index) 
        {
            return reader.IsDBNull(index) ?   false : reader.GetBoolean(index);
        }

        public bool ConvertDBValueToBooleanNullable(FbDataReader reader, int index)
        {
            return ConvertDBValueToBoolean(reader, index);
        }

        public byte ConvertDBValueToByte(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ?  byte.MinValue : reader.GetByte(index);
        }

        public byte?  ConvertDBValueToByteNullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (byte?)reader.GetByte(index);
            
        }

        public byte[] ConvertDBValueToBytes(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (byte[])reader[index];
        }

        public byte[] ConvertDBValueToBytesNullable(FbDataReader reader, int index)
        {
            return ConvertDBValueToBytes(reader, index);
        }

        public DateTime ConvertDBValueToDateTime(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? DateTime.MinValue : reader.GetDateTime(index);
        }

        public DateTime?  ConvertDBValueToDateTimeNullable(FbDataReader reader, int index)
        {
           return reader.IsDBNull(index) ? null : (DateTime?)reader.GetDateTime(index);
           
        }

        public decimal ConvertDBValueToDecimal(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : reader.GetDecimal(index);
        }

        public decimal?  ConvertDBValueToDecimalNullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (decimal?)reader.GetDecimal(index);
        }

        public double ConvertDBValueToDouble(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : reader.GetDouble(index);
        }

        public double?  ConvertDBValueToDoubleNullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (double?)reader.GetDouble(index);
        }

        public float ConvertDBValueToFloat(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : reader.GetFloat(index);
        }

        public float?  ConvertDBValueToFloatNullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (float?)reader.GetFloat(index);
        }

        public short ConvertDBValueToInt16(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? short.MinValue : reader.GetInt16(index);
        }

        public short?  ConvertDBValueToInt16Nullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (Int16?)reader.GetInt16(index);
        }

        public int ConvertDBValueToInt32(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? 0 : reader.GetInt32(index);
        }

        public int?  ConvertDBValueToInt32Nullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (Int32?)reader.GetInt32(index);
        }

        public long ConvertDBValueToInt64(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? long.MinValue : reader.GetInt64(index);
        }

        public long?  ConvertDBValueToInt64Nullable(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : (Int64?)reader.GetInt64(index);
        }

        public string ConvertDBValueToString(FbDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? String.Empty : reader.GetString(index);
        }

        public string  ConvertDBValueToStringNullable(FbDataReader reader, int index)
        {
            return ConvertDBValueToString(reader, index);
        }

        public short ConvertDBValueToShortNullable(FbDataReader reader,int index)
        {
            return ConvertDBValueToShortNullable(reader, index);
        }

       
    }
}

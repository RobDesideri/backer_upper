﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackerUpper
{
    class Settings
    {
        Database db;

        public string Source {
            get { return this.getKey("source"); }
            set { this.setKey("source", value); }
        }

        public bool MirrorEnabled {
            get { return (this.getKey("mirrorEnabled") == "1"); }
            set { this.setKey("mirrorEnabled", value ? "1" : "0"); }
        }

        public string MirrorDest {
            get { return this.getKey("mirrorDest"); }
            set { this.setKey("mirrorDest", value); }
        }

        public bool S3Enabled {
            get { return (this.getKey("s3Enabled") == "1"); }
            set { this.setKey("s3Enabled", value ? "1" : "0"); }
        }

        public string S3Dest {
            get { return this.getKey("s3Dest"); }
            set { this.setKey("s3Dest", value); }
        }

        public string S3PublicKey {
            get { return this.getKey("s3PublicKey"); }
            set { this.setKey("s3PublicKey", value); }
        }

        public string S3PrivateKey {
            get { return this.getKey("s3PrivateKey"); }
            set { this.setKey("s3PrivateKey", value); }
        }

        public string Name {
            get { return this.getKey("name"); }
            set { this.setKey("name", value); }
        }

        public Settings(Database db) {
            this.db = db;
        }

        private void setKey(string name, string value) {
            this.db.Execute("UPDATE settings SET value = @value WHERE name = @name", "@name", name, "@value", value);
        }

        private string getKey(string name) {
            return this.db.ExecuteScalar("SELECT value FROM settings WHERE name = @name", "@name", name);
        }

        public void PopulateInitial(string name) {
            this.db.Execute(@"INSERT INTO settings(name, value) VALUES
                ('name', @name), ('source', ''),
                ('mirrorEnabled', '0'), ('mirrorDest', ''),
                ('s3Enabled', '0'), ('s3Dest', ''), ('s3PublicKey', ''), ('s3PrivateKey', '');", "@name", name);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace cache_db.Commands { 
    public class Actions {

        public Actions() {
            this.Properties = new Dictionary<string, string>();
            this.Backups = new Stack<Dictionary<string, string>>();
        }

        private Dictionary<string, string> Properties { get; set; }
        public Stack<Dictionary<string, string>> Backups { get; set; }
        
        public void Set(string key, string value) {
            if (this.Properties.ContainsKey(key)) {
                this.Properties[key] = value;
            } else {
                this.Properties.Add(key, value);
            }
        }

        public void Unset(string key) {
            this.Set(key, "NULL");
        }

        public int EqualTo(string value) =>
            this.Properties.Values.Where(x => x.Equals(value)).Count();

        public string Get(string key) {
            var value = this.Properties.GetValueOrDefault(key);
            return string.IsNullOrEmpty(value) ? "0" : value;
        }

        public void Begin() {
            this.Backups.Push(new Dictionary<string, string>(this.Properties));
        }

        public void Commit() {
            this.Backups = new Stack<Dictionary<string, string>>();
        }

        public void RollBack() {
            if (this.Backups.Count == 0) {
                throw new Exception("NO TRANSACTION");
            }

            this.Properties = this.Backups.Pop();
        }
    }
}
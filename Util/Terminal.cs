using System;
using System.Linq;
using cache_db.Commands;

namespace cache_db.Util
{
    public class Terminal
    {
        public Terminal(string consoleText) {
            this._commands = consoleText.Split(' ');
            this.MapParrams();
        }

        private string[] _commands { get; set; }
        public DataCommand DataCommand { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Command { get; set; }
        
        
        
        
        public string GetDataCommand() {
            return this._commands[0];
        }

        public void MapParrams() {
            DataCommand dataCommand;
            var hasCommand = Enum.TryParse(this._commands[0], true, out dataCommand);
            if (!hasCommand) {
                throw new Exception(string.Format("Command {0} not found", this._commands[0]));
            }

            this.Command = this._commands[0];
            this.DataCommand = dataCommand;

            if (this.DataCommand.Equals(DataCommand.SET)) {
                if (this._commands.Length == 1) {
                    throw new Exception(string.Format("Expect 'name' param to command {0}", this._commands[0]));
                }
                
                if (this._commands.Length == 2) {
                    throw new Exception(string.Format("Expect 'value' param to command {0}", this._commands[0]));
                }

                this.Key = this._commands[1];
                this.Value = this._commands[2];
                return;
            }

            var DataCommandsWithOneParameter = new DataCommand[] { DataCommand.GET, DataCommand.UNSET, DataCommand.NUMEQUALTO };
            if (DataCommandsWithOneParameter.Contains(this.DataCommand)) {
                
                var isNumEqualTo = this.DataCommand.Equals(DataCommand.NUMEQUALTO);

                if (this._commands.Length <= 1) {
                    var paramError = isNumEqualTo ? "name" : "value";
                    throw new Exception(string.Format("Expect '{0}' param to command {1}", paramError, this._commands[0]));
                }

                if (isNumEqualTo) {
                    this.Value = this._commands[1];
                } else {
                    this.Key = this._commands[1];
                }
            }
        }
    }
}
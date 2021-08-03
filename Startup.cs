using System;
using System.Linq;
using cache_db.Commands;
using cache_db.Util;

namespace cache_db {
    public class Startup {

        private readonly Actions _action;

        public Startup() {
            if (_action == null) {
                _action = new Actions();
            }
        }

        public void Execute() {
            try {
                var stdin = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stdin)) {
                    this.Execute();
                    return;
                };

                Terminal terminal = new Terminal(stdin);

                switch (terminal.DataCommand) {
                    case DataCommand.SET: 
                        this._action.Set(terminal.Key, terminal.Value);
                        break;
                    case DataCommand.GET:
                        Console.WriteLine(this._action.Get(terminal.Key));
                        break;
                    case DataCommand.NUMEQUALTO:
                        Console.WriteLine(this._action.EqualTo(terminal.Value));
                        break;
                    case DataCommand.UNSET:
                        this._action.Unset(terminal.Key);
                        break;
                    case DataCommand.BEGIN:
                        this._action.Begin();
                        break;
                    case DataCommand.ROLLBACK:
                        this._action.RollBack();
                        break;
                    case DataCommand.COMMIT:
                        this._action.Commit();
                        break;
                    case DataCommand.END:
                        return;
                    default:
                        Console.WriteLine(string.Format("Command {0} not found", terminal.Command));
                        break;
                }

                this.Execute();
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                this.Execute();
                return;
            }
        }
    }
}
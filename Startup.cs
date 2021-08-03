using System;
using System.Linq;
using cache_db.Commands;
using cache_db.Util;

namespace cache_db {
    public class Startup {

        private static Actions _action;

        private Startup() { }

        private static void CreateActionInstance() {
            if (_action == null) {
                _action = new Actions();
            }
        }

        public static void Execute() {
            try {
                var stdin = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stdin)) {
                    Execute();
                    return;
                };

                Terminal terminal = new Terminal(stdin);

                CreateActionInstance();
                switch (terminal.DataCommand) {
                    case DataCommand.SET: 
                        _action.Set(terminal.Key, terminal.Value);
                        break;
                    case DataCommand.GET:
                        Console.WriteLine(_action.Get(terminal.Key));
                        break;
                    case DataCommand.NUMEQUALTO:
                        Console.WriteLine(_action.EqualTo(terminal.Value));
                        break;
                    case DataCommand.UNSET:
                        _action.Unset(terminal.Key);
                        break;
                    case DataCommand.BEGIN:
                        _action.Begin();
                        break;
                    case DataCommand.ROLLBACK:
                        _action.RollBack();
                        break;
                    case DataCommand.COMMIT:
                        _action.Commit();
                        break;
                    case DataCommand.END:
                        return;
                    default:
                        Console.WriteLine(string.Format("Command {0} not found", terminal.Command));
                        break;
                }

                Execute();
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                Execute();
                return;
            }
        }
    }
}
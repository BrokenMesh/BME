using BME.GameLoop;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BME.Util
{
    public class DataFile
    {
        private List<string?> data;
        private Dictionary<string, DataFile> children;
        public bool isComment = false;

        public DataFile() {
            data = new List<string?>();
            children = new Dictionary<string, DataFile>();
        }

        public DataFile Get(string _name) {
            if (!children.ContainsKey(_name)) {
                children.Add(_name, new DataFile());
            }

            return children[_name];
        }

        public DataFile GetPath(string _path) {
            string[] _nodes = _path.Trim().Split('/');

            if (_nodes.Length == 1)
                return Get(_nodes[0]);

            string _newPath = _path.Remove(0, _nodes[0].Length + 1);
            return Get(_nodes[0]).GetPath(_newPath);
        }

        public void AddComment(string _comment) {
            if (String.IsNullOrEmpty(_comment)) return;

            DataFile _commentNode = new DataFile();
            _commentNode.isComment = true;
            children.Add("#" + _comment, _commentNode);
        }

        #region//_READ_AND_WRITE

        private static void WriteNode(DataFile _df, StreamWriter _writer, string _indent, string _listSep, uint _indentCount) {
            string _indentStr = "";
            for (int i = 0; i < _indentCount; i++) {
                _indentStr += _indent;
            }
            
            foreach (string _ckey in _df.children.Keys) { 
                DataFile _c = _df.children[_ckey];

                if (_c.isComment) {
                    _writer.Write(_indentStr);
                    _writer.Write(_ckey);
                    _writer.Write("\n");
                    continue;
                }

                if (_c.children.Count == 0) { // Write Node-Data
                    _writer.Write(_indentStr);
                    _writer.Write(_ckey);
                    _writer.Write(" = ");

                    int _numData = _c.data.Count();
                    foreach (string? _data in _c.data) {
                        if (_data == null) continue;

                        if (_data.Contains(_listSep)) {
                            _writer.Write(" \"" + _data + "\"");
                        }
                        else {
                            _writer.Write(_data);
                        }

                        if (_numData > 1) _writer.Write(_listSep);
                        _numData--;
                    }

                    _writer.Write("\n");

                } else { // Write Node
                    _writer.Write(_indentStr);
                    _writer.Write(_ckey);
                    _writer.Write("\n");

                    _writer.Write(_indentStr);
                    _writer.Write("{\n");

                    WriteNode(_c, _writer, _indent, _listSep, _indentCount+1);

                    _writer.Write(_indentStr);
                    _writer.Write("}\n\n");
                }
            }

        }

        public static void Write(DataFile _df, string _file, string _indent = "\t", char _listSep = ',') {
            StreamWriter _writer = new StreamWriter(_file, false);

            WriteNode(_df, _writer, _indent, _listSep + " ", 0);

            _writer.Close();
        }

        public static DataFile? Read(string _file, char _listSep = ',') {
            if (!File.Exists(_file)) return null; 

            StreamReader _reader = new StreamReader(_file);

            Stack<DataFile> _dfStack = new Stack<DataFile>();
            _dfStack.Push(new DataFile());

            string _nodeName = "";

            while (!_reader.EndOfStream) { 
                string? _line = _reader.ReadLine();
                if (_line == null) continue;

                _line = _line.Trim();
                if (String.IsNullOrEmpty(_line)) continue;

                if (_line.StartsWith("#")) {
                    DataFile _comment = new DataFile();
                    _comment.isComment = true;
                    _dfStack.Peek().children.Add(_line, _comment);
                }

                if (_line.Contains("=")) { // Read Node-Data
                    string _name = _line.Split("=")[0].Trim();
                    string _value = _line.Split("=")[1].Trim();

                    string _curData = "";
                    bool isInQuotes = false;
                    foreach (char _c in _value) {
                        if (_c == '"') {
                            isInQuotes = !isInQuotes;
                            continue;
                        }

                        if (_c == _listSep && !isInQuotes) {
                            _dfStack.Peek().Get(_name).SetString(_curData.Trim());
                            _curData = "";
                            continue;
                        }

                        _curData += _c;
                    }

                    if (!String.IsNullOrEmpty(_curData)) {
                        _dfStack.Peek().Get(_name).SetString(_curData.Trim());
                    }
                }
                else if (_line == "{") {
                    _dfStack.Push(_dfStack.Peek().Get(_nodeName));
                }
                else if (_line == "}") {
                    _dfStack.Pop();
                } 
                else { 
                    _nodeName = _line;
                }
            }
            _reader.Close();

            if (_dfStack.Count != 1) return null;
            return _dfStack.Pop();
        }

        #endregion//_READ_AND_WRITE

        #region//GETTERS_AND_SETTERS

        // Setters
        public void SetString(string _value, int _index) {
            while (data.Count <= _index) {
                data.Add(null);
            }    

            data[_index] = _value;            
        }

        public void SetString(string _value) {
            for (int i = 0; i < data.Count; i++) {
                if (data[i] == null) {
                    data[i] = _value;
                    return;
                }
            }

            data.Add(_value);
        }

        public void SetInt(int _value, int _index) {
            SetString(_value.ToString(), _index);
        }

        public void SetInt(int _value) {
            SetString(_value.ToString());
        }

        public void SetFloat(float _value, int _index) {
            SetString(_value.ToString(), _index);
        }

        public void SetFloat(float _value) {
            SetString(_value.ToString());
        }

        // Getters

        public string? GetString(int _index) {
            if(_index >= data.Count)
                return null;

            return data[_index];
        }

        public string? GetString() { 
            return GetString(0);
        }

        public List<string> GetDataList() {
            return data;
        }

        public int? GetInt(int _index) {
            if (int.TryParse(GetString(_index), out int _result)) {
                return _result;
            }
            return null;
        }
        public float? GetFloat(int _index) {
            if (float.TryParse(GetString(_index), out float _result)) {
                return _result;
            }
            return null;
        }

        public int GetValueCount() {
            return data.Count;
        }

        #endregion//GETTERS_AND_SETTERS


    }
}

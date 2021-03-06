//  This file is part of YamlDotNet - A .NET library for YAML.
//  Copyright (c) 2008, 2009, 2010, 2011, 2012, 2013 Antoine Aubry
    
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//  of the Software, and to permit persons to whom the Software is furnished to do
//  so, subject to the following conditions:
    
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
    
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.

using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace YamlDotNet.Test
{
	public static class Yaml
	{
		public static TextReader StreamFrom(string name)
		{
			var fromType = typeof(Yaml);
			var assembly = Assembly.GetAssembly(fromType);
			var stream = assembly.GetManifestResourceStream(name) ??
						 assembly.GetManifestResourceStream(fromType.Namespace + ".files." + name);
			return new StreamReader(stream);
		}

		public static string TemplatedOn<T>(this TextReader reader)
		{
			var text = reader.ReadToEnd();
			return text.TemplatedOn<T>();
		}

		public static string TemplatedOn<T>(this string text)
		{
			return Regex.Replace(text, @"{type}", match =>
				Uri.EscapeDataString(String.Format("{0}, {1}", typeof(T).FullName, typeof(T).Assembly.FullName)));
		}
	}
}
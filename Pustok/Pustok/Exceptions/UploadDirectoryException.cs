using Pustok.Contracts;
using System;

namespace Pustok.Exceptions;

public class UploadDirectoryException : ApplicationException
{
	public UploadDirectory UploadDirectory { get; set; }

	public UploadDirectoryException(string message, UploadDirectory uploadDir)
		:base(message)
	{
        UploadDirectory = uploadDir;
    }
}

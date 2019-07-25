Solution to the Kiwiland Train Network Problem

Marc Adler
magmasystems@gmail.com
April 15, 2015

This solution was written in response to the ThoughtWorks programming exercise, and this solution solves the first problem, 
which has to do with answering questions reagrding a fictional railway network in the district of Kiwiland.

This solution contains a number of classes (mainly around binary trees) that are not used in this problem, but are part of
a general framework that I was writing in preparation for the ThoughtWorks interviewing process. As such, this solution is
is still very much a work-in-progress.

The solution was written in C# 4.5 and requires .NET 4.51 and Visual Studio 2013. Those are the only dependencies. The source
code to the solution is hosted in TFS on Visual Studio Online. If you receive any warning about source control when you open
the solution, then click on the appropriate options in the dialog box to disconnect the solution from source control.

The main program, a Console application, is in the MagmaGraph.Application project. In Visual Studio, make sure that this is the 
Startup Project. (In Solution Explorer, right-click on the MagmaGraph.Application project and choose the Set as StartUp Project
option.) Run the console application. The answers to the ten questions will be printed to the console, and then a message will
appear, asking you to press the ENTER key to exit the application.

Each of the projects (except for the main application) has corresponding unit tests. You can run all unit tests from Visual Studio
by using the built-in Test Explorer.

The assumptions that I have made include:

1) Names of the train stops can be any string, not limited to a single letter.

2) The distances are integers. The WeightedEdge<int> (or the derived IntegerWeightedEdge) is used for this. There is also
a subclass called DoubleWeightedEdge. It would be nice if C# supported an interface called IArithmetical or ICalculatable
which supported integer or floating-point arithmetic. There are some third-party attempts to address this, but nothing official
from Microsoft.


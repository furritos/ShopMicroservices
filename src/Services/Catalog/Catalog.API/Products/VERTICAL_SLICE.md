# VERTICAL SLICE

With Vertical Slice architecture, the common approach is to encapsulate 
ALL aspects of a feature into once class.

However, that makes the code hard to read/maintain/localte.  Instead of that,
the Presentation layer and Application Logic has been split into their own class
file to remediate the obstacles noted above in keeping everything in a file.

*Endpoint.cs is the Presentation Layer
*Handler.cs is the Application Logic Layer
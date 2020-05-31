# ChaD: Simple chat application using .net sockets

## Working:
* Connection to server
* Changing nickname on server
* Retrieving list of active users (on request by now)
* Sending/receiving direct messages
* Sending/receiving broadcasts 

## TODO:
* Wrap command parser for both server & client
* Client-side container for active users
* Event-driven syncronization between server-client
* Auto-disconect on timeout
* App Journaling (e.g. logging)



## Requirements:
* .net5 (or retarget build to .netCore3.1)

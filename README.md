# Code-RetroSpective
During a 2 week period me and a team worked on a sprint where we were able to work on front end stories, back end stories, and bugs.

For this story I was tasked with Changing a Javascript clock to a clock that uses Moment.js. I spent a few days researching the syntax for moment.js and came across an error with a fitText which when removed the clock displayed. I then had to move the javascript to an external file. I ran into problems moving the code to the main site.js file(I believe this is because the functions in the site.js file were unamed), so I created a new file.

![moment script](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_1.PNG)

This code enables Moment and Jquery code on this page.


![Html display](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_2.PNG)

Code that displays the clock. Id clock connects this line of Html with the moment.js code.


![Update clock](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_4.PNG)

the setInterval method updates the clock so that it stays current. it updates every 200 miliseconds to prevent the clock from lagging.


![moment clock](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_6.PNG)

Code that gets the current time using moment.js.

For the next story I was tasked with Adding an approve function to the timeOffEvent. I talked to the project manager and he set some tasks for me the main one being to set the userId to approverId when Approve button is hit. The TimeOffEvent is an admin only controller, so it would set admin ID to approverID. I had to do research for the exact syntax and dug through the db to make sure I used the right table.

![UserId to approverId](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_5.PNG)


This was a simple bug fix, I had to hide the password when typed. Changed 'TextBoxFor' to 'PasswordFor'

![Hide password](https://github.com/Addaku/Code-RetroSpective/blob/master/Code_3.PNG)

# Code-RetroSpective
During the past 2 weeks me and and a team worked on a sprint where we were able to work on front end stories, back end stories, and bugs.

For this story I was tasked with Changing a Javascript clock to a clock that uses Moment.js. I spent a few days researching the syntax for moment.js and came across an error with a fitText which when removed the clock displayed. I then had to move the javascript to an external file. I ran into problems moving the code to the main site.js file(I believe this is because the functions in the site.js file were unamed), so I created a new file.
```
<head>
    <script src="~/Scripts/jquery-3.3.1.js"></script>
    <script src="~/Scripts/moment.js"></script>
    <script src="~/Scripts/Clock.js"></script>
</head>

//Displays clock through ID clock
<div id="clock" class="pill-clock">&nbsp</div>

<script>
//updates clock every 200 milliseconds, located in the cshtml file
setInterval(updateClock, 200) 
</script>
```
Clock.js file
```
function updateClock() {
	$('#clock').html(moment().format('h:mm:ss a'));
};
```
For this story I was tasked with Adding an approve function to the timeOffEvent. I talk to the project manager and he set some tasks for me the main one being to set the userId to approverId when Approve button is hit. The TimeOffEvent is an admin only controller, so it would set admin ID to approverID. I had to do research for the exact syntax and dug through the db to make sure I use the right table.
```
public ActionResult Approve(Guid id)
{ 
TimeOffEvent timeOffEvent = db.TimeOffEvents.Find(id);
	timeOffEvent.ApproverId = new Guid(User.Identity.GetUserId());
	db.SaveChanges();
	return RedirectToAction("Index");
}
```
This was a simple bug fix, I had to hide the password when typed. Changed 'TextBoxFor' to 'PasswordFor'
```
<div class="col-md-10">
 @Html.PasswordFor(m => m.Password, new { @class = "form-control" })
 @Html.ValidationMessageFor(m => m.Password, "", new { @class = "text-danger" })
</div>
```

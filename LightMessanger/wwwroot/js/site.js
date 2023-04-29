

	var dropdown = document.getElementById('add-group');


	var modal = document.getElementById('modal');

	var closeBtn = document.querySelector('.close');


	dropdown.addEventListener('click', function() {
	  modal.style.display = 'block';
	});


	closeBtn.addEventListener('click', function() {
	  modal.style.display = 'none';
	});

	window.addEventListener('click', function(event) {
	  if (event.target == modal) {
		modal.style.display = 'none';
	  }
	});

	var myDiv =  document.getElementById("messages");
	if(myDiv)
	{
		myDiv.scrollTo(0, myDiv.scrollHeight);
	}
  


	var myDiv = document.getElementById("@ViewBag.Chat");
	if(myDiv)
	{
		myDiv.classList.toggle("active-chat");
	}

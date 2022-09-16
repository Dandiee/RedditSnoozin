//META{"name":"Example"}*//

class Example {
	
	getNameNick() {
		return "szotyí";
	}
	
	
    // Constructor
    constructor() {
        this.initialized = false;
    }

    // Meta
    getName() { return "Example"; }
    getShortName() { return "Example"; }
    getDescription() { return "This is an example/template for a BD plugin."; }
    getVersion() { return "0.1.0"; }
    getAuthor() { return "Minin"; }

    // Settings  Panel
    getSettingsPanel() {
        return "<!--Enter Settings Panel Options, just standard HTML-->";
    }
    
    // Load/Unload
    load() { 
	
		this.cleanShitUp();
		
		document.addEventListener('keydown', (event) => {
		  var name = event.key;
		  var code = event.code;
		  
		  if (name === 'F7') {
			  document.getElementById("app-mount").style.opacity = "1.0";
		  }
		  
		}, false);
	
	}

    unload() { }

    // Events

    onMessage() {
        this.cleanShitUp();
    };

    onSwitch() {
		this.cleanShitUp();
		
		var html = document.documentElement.innerHTML;
		
		if (html.includes("Message @ada4sure")) {
			document.getElementById("app-mount").style.opacity = "0.0";
		}
		else{ 

			document.getElementById("app-mount").style.opacity = "1.0";
		}
    };

    observer(e) {
        
		this.cleanCheapShitUp();
		
    };
    
    // Start/Stop
    start() {
        var libraryScript = document.getElementById('zeresLibraryScript');
	if (!libraryScript) {
		libraryScript = document.createElement("script");
		libraryScript.setAttribute("type", "text/javascript");
		libraryScript.setAttribute("src", "https://rauenzi.github.io/BetterDiscordAddons/Plugins/PluginLibrary.js");
		libraryScript.setAttribute("id", "zeresLibraryScript");
		document.head.appendChild(libraryScript);
	}

	if (typeof window.ZeresLibrary !== "undefined") this.initialize();
	else libraryScript.addEventListener("load", () => { this.initialize(); });
    }
       
    stop() {
        //PluginUtilities.showToast(this.getName() + " " + this.getVersion() + " has stopped.");
    };

    //  Initialize
    initialize() {
        this.initialized = true;
        //PluginUtilities.showToast(this.getName() + " " + this.getVersion() + " has started.");
    }
	
	cleanCheapShitUp() {
		try {
			var titles = document.getElementsByTagName('title');
			if (titles) {
				
				for (var i = 0; i < titles.length; i++) {
					if (titles[i].innerText === "ada4sure") {
						titles[i].innerText  = this.getNameNick();
					}
				}
				
			}
			
			
			var dmUsernames = document.querySelectorAll('[class^="username-"]');
			if (dmUsernames) {
				for (var i = 0; i < dmUsernames.length; i++) {
					if (dmUsernames[i].innerText === "ada4sure") {
						dmUsernames[i].innerText  = this.getNameNick();
					}
				}
			}
			
			
			var typingDots = document.querySelector('[class^="typingDots-"]');
			if (typingDots) {
				var typingNameStrong = typingDots.getElementsByTagName("strong");
				if (typingNameStrong.innerText == "ada4sure") {
					
					typingNameStrong.innerText = this.getNameNick();
					
				}
			}
					
			var tooltips = document.querySelectorAll('div [class^="tooltipContent-"]');
			if (tooltips) {
				for (var i = 0; i < tooltips.length; i++) {
					var tooltip2 = tooltips[i];				
					if (tooltip2.innerText == "ada4sure") {
						tooltips.innerText = this.getNameNick();
					}
				}
				
			}
			
			var avatars = document.querySelectorAll('img[src$="c09a43a372ba81e3018c3151d4ed4773.png"]');
			if (avatars) {
				for ( var i = 0; i < avatars.length; i++) {
					var originalAvatar = avatars[i].src;
					avatars[i].src = originalAvatar.replace("c09a43a372ba81e3018c3151d4ed4773", "6f26ddd1bf59740c536d2274bb834a05");
				}
			}
			
			// var typings = document.querySelectorAll('string[text()="ada4sure"]');
			// if (typings) {
				// for ( var i = 0; i < typings.length; i++) {
					// typings[i].innerText = this.getNameNick();
				// }
			// }
			
			
		}
		catch {
			console.log("a kcsiben is");
		}
	}
	
	cleanShitUp() {
		
		try
		{
		
			var friendListAda = document.querySelector('[data-list-item-id="people___288374217945251841"]');
			if (friendListAda) {
				friendListAda.getElementsByTagName("span")[0].innerText = this.getNameNick();
				friendListAda.getElementsByTagName("img")[0].src = "https://discord.com/assets/6f26ddd1bf59740c536d2274bb834a05.png";	
			}
			
			
			var dmListAda =  document.querySelector('a[href="/channels/@me/700401634240495656"]');
			if (dmListAda) {		
				dmListAda.children[0].children[1].children[0].children[0].children[0].innerText = this.getNameNick();
				//dmListAda.querySelector('[class^="overflow-"]').innerText = this.getNameNick();
			}
			
			
			var dmheader = document.getElementsByTagName('h3');
			if (dmheader) {
				
				for (var i = 0; i < dmheader.length; i++) {
					if (dmheader[i].innerText === "ada4sure") {
						dmheader[i].innerText  = this.getNameNick();
					}
				}
				
			}
		
		}
		catch{
			console.log("gecire elkúrtam valamit");
		}
		
		this.cleanCheapShitUp();
	}
}
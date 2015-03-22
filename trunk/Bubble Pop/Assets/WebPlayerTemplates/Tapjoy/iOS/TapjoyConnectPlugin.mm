#import "TapjoyConnectPlugin.h"
#import "TapjoyEventPlugin.h"

UIViewController *UnityGetGLViewController();

static TapjoyConnectPlugin *_sharedInstance = nil; //To make TapjoyConnect Singleton

@implementation TapjoyConnectPlugin

@synthesize cSharpDictionaryRefs = cSharpDictionaryRefs_, callbackHandlerName = callbackHandlerName_, displayAdSize = displayAdSize_, displayAdOrientation = displayAdOrientation_, displayAdFrame = displayAdFrame_;
@synthesize eventsDict = eventsDict_;
@synthesize eventRequestDict = eventRequestDict_;

@synthesize enableDisplayAdAutoRefresh;
  
NSString *const CONNECT_FLAG_KEY = @"connectFlags";

+ (void)initialize
{
	if (self == [TapjoyConnectPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (TapjoyConnectPlugin*)sharedTapjoyConnectPlugin
{
	return _sharedInstance;
}


- (id)init
{
	self = [super init];
    
    if (self)
    {
        tapPoints = 0;
        displayAdSize_ = TJC_DISPLAY_AD_SIZE_320X50;
        displayAdFrame_ = CGRectMake(0, 0, 320, 50);
        cSharpDictionaryRefs_ = [[NSMutableDictionary alloc] init];

        [Tapjoy setViewDelegate:self];
        [Tapjoy setVideoAdDelegate:self];
        
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(tjcConnectSuccess:)
                                                     name:TJC_CONNECT_SUCCESS
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(tjcConnectFail:) name:TJC_CONNECT_FAILED object:nil];
        
        // Add an observer for when Tap Points has been successfully retrieved.
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(getUpdatedPoints:)
                                                     name:TJC_TAP_POINTS_RESPONSE_NOTIFICATION
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(spendPoints:)
                                                     name:TJC_SPEND_TAP_POINTS_RESPONSE_NOTIFICATION
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(awardPoints:)
                                                     name:TJC_AWARD_TAP_POINTS_RESPONSE_NOTIFICATION
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(getUpdatedPointsError:)
                                                     name:TJC_TAP_POINTS_RESPONSE_NOTIFICATION_ERROR
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(spendPointsError:)
                                                     name:TJC_SPEND_TAP_POINTS_RESPONSE_NOTIFICATION_ERROR
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self 
                                                 selector:@selector(awardPointsError:) 
                                                     name:TJC_AWARD_TAP_POINTS_RESPONSE_NOTIFICATION_ERROR 
                                                   object:nil];
        
        // Add an observer for when a user has successfully earned currency.
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(showEarnedCurrencyAlert:) 
                                                     name:TJC_TAPPOINTS_EARNED_NOTIFICATION 
                                                   object:nil];
        
        // Get fullscreen ad Call
        [[NSNotificationCenter defaultCenter] addObserver:self 
                                                 selector:@selector(getFullScreenAd:) 
                                                     name:TJC_FULL_SCREEN_AD_RESPONSE_NOTIFICATION 
                                                   object:nil];

        [[NSNotificationCenter defaultCenter] addObserver:self 
                                                 selector:@selector(getFullScreenAdError:) 
                                                     name:TJC_FULL_SCREEN_AD_RESPONSE_NOTIFICATION_ERROR
                                                   object:nil];

        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(showOffersError:)
                                                     name:TJC_OFFERS_RESPONSE_NOTIFICATION_ERROR
                                                   object:nil];
    }
	return self;
}

#pragma mark Bridge methods between C# and Objective-C
  
// Sets a value to a key in specific dictionary
- (void)setKey:(NSString*)key ToValue:(NSString*)value InDictionary:(NSString*)dictionaryToAddTo
  {
    // Get dictionary to add key and value to, creates one if doesn't exist
    NSMutableDictionary* currentDictionary = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryToAddTo];
    
    [currentDictionary setObject:value forKey:key];
  }
  
// Sets a dictionary as a vaule to a key in specific dictionary
- (void)setKey:(NSString*)key ToDictionaryRefValue:(NSString*)dictionaryRefToAdd InDictionary:(NSString*)dictionaryToAddTo
  {
    // Get dictionary to add key and value to, creates one if doesn't exist
    NSMutableDictionary* dictionaryToTransferTo = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryToAddTo];
    
    // Get reference to dictionary that needs to be added
    NSMutableDictionary* dictionaryToBeSetAsValue = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryRefToAdd AndCreateNewInstance:NO];
    if (!dictionaryToBeSetAsValue) {
      NSLog(@"no dictionary reference by the name of: %@", dictionaryRefToAdd);
      return;
    }
    
    [dictionaryToTransferTo setObject:dictionaryToBeSetAsValue forKey:key];
  }
  
// Helper function to check if a dictionary is defined and if not creates one
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName
  {
    return [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryName AndCreateNewInstance:YES];
  }
  
// Helper function to check if a dictionary is defined and will or will not create a new instance if one doesn't exsits
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName AndCreateNewInstance:(BOOL) newInstance
  {
    NSMutableDictionary* currentDictionary = [cSharpDictionaryRefs_ objectForKey:dictionaryName];
    
    if (!currentDictionary && newInstance)
    {
      currentDictionary = [[NSMutableDictionary alloc] init];
      [cSharpDictionaryRefs_ setObject:currentDictionary forKey: dictionaryName];
    }
    return currentDictionary;
  }

#pragma mark Tapjoy callbacks
- (void)tjcConnectSuccess:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapjoyConnectSuccess", "success");
}


- (void)tjcConnectFail:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapjoyConnectFail", "failure");
}


- (void)getUpdatedPoints:(NSNotification*)notifyObj
{
  tapPoints = [notifyObj.object intValue];
	
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsLoaded", [[NSString stringWithFormat:@"%i", tapPoints] UTF8String]);
}


- (void)getUpdatedPointsError:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsLoadedError", "Error loading Tap Points");
}

- (void)spendPoints:(NSNotification*)notifyObj
{
	tapPoints = [notifyObj.object intValue];
    
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsSpent", [[NSString stringWithFormat:@"%i", tapPoints] UTF8String]);
}


- (void)spendPointsError:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsSpendError", "Error spending Tap Points");
}


- (void)awardPoints:(NSNotification*)notifyObj
{
	tapPoints = [notifyObj.object intValue];
	
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsAwarded", [[NSString stringWithFormat:@"%i", tapPoints] UTF8String]);
}


- (void)awardPointsError:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "TapPointsAwardError", "Error awarding Tap Points");
}


- (int)queryTapPoints
{
	return tapPoints;
}


- (void)getFullScreenAd:(NSNotification*)notifyObj
{
	// notifyObj will be returned as Nil in case of internet error or unavailibity of fullscreen ad
	// or its Max Number of count has exceeded its limit
	UnitySendMessage([callbackHandlerName_ UTF8String], "FullScreenAdLoaded", "success");
}


- (void)getFullScreenAdError:(NSNotification*)notifyObj
{
	// notifyObj will be returned as Nil in case of internet error or unavailibity of fullscreen ad
	// or its Max Number of count has exceeded its limit
	UnitySendMessage([callbackHandlerName_ UTF8String], "FullScreenAdError", "failure");
}

- (void)showEarnedCurrencyAlert:(NSNotification*)notifyObj
{
	NSNumber *tapPointsEarned = notifyObj.object;
	earnedCurrencyAmount = [tapPointsEarned intValue];
	
	UnitySendMessage([callbackHandlerName_ UTF8String], "CurrencyEarned", [[NSString stringWithFormat:@"%i", earnedCurrencyAmount] UTF8String]);
}

- (void)showOffersError:(NSNotification*)notifyObj
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ShowOffersError", "failure");
}

- (void)moveDisplayAdToX:(int)x toY:(int)y
{
	displayAdFrame_.origin = CGPointMake(x, y);
}


- (void)showDisplayAd
{
	// Get the TJCAdView, which is a subclass of UIView.
    TJCAdView *adView = [Tapjoy getDisplayAdView];
    // Set the frame, in case moveDisplayAd.. has changed it.
	[adView setFrame:displayAdFrame_];
    // Get the Unity GL ViewController
    UIViewController *glView = UnityGetGLViewController();
    [glView.view addSubview:(UIView *)adView];
}


- (void)hideDisplayAd
{
	UIView *adView = (UIView*)[Tapjoy getDisplayAdView];
	
	[adView removeFromSuperview];
}


- (void)dealloc
{
	[super dealloc];
}

- (BOOL)shouldRefreshAd
{
       return [self shouldDisplayAdAutoRefresh];
}

#pragma mark Tapjoy Display Ads Delegate Methods

- (void)didReceiveAd:(TJCAdView*)adView
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "DisplayAdLoaded", "success");
}


- (void)didFailWithMessage:(NSString*)msg
{
	NSLog(@"No Tapjoy Display Ads available");
}


- (NSString*)adContentSize
{
	return displayAdSize_;
}


#pragma mark Tapjoy Video Ads Delegate Methods

- (void)videoAdBegan
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "VideoAdStart", "Video Ad has started");
}

- (void)videoAdClosed
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "VideoAdComplete", "Video Ad has been closed");
}

- (void)videoAdCompleted
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "VideoAdComplete", "Video Ad has been completed");
}

- (void)videoAdError:(NSString *)errorMsg
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "VideoAdError", [errorMsg UTF8String]);
}


#pragma mark Tapjoy View Delegate Methods

- (void)viewDidAppearWithType:(int)viewType
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ViewOpened", [[NSString stringWithFormat:@"%i", viewType] UTF8String]);
}

- (void)viewDidDisappearWithType:(int)viewType
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ViewClosed", [[NSString stringWithFormat:@"%i", viewType] UTF8String]);
}

#pragma mark Tapjoy Event Methods

- (void)createEventWithGuid:(NSString *)guid name:(NSString *)name parameter:(NSString *)parameter
{
	// Create dictionary if its empty
	if (!eventsDict_)
		eventsDict_ = [[NSMutableDictionary alloc] init];
	
	// TODO: error check on guid
	TapjoyEventPlugin *tjevt = [TapjoyEventPlugin createEventWithGuid:guid];

	TJEvent *evt;
	if(!parameter || parameter == (id)[NSNull null] || parameter.length == 0)
		evt = [TJEvent eventWithName:name delegate:tjevt];
	else
		evt = [TJEvent eventWithName:name value:parameter delegate:tjevt];
	
	[eventsDict_ setObject:evt forKey:guid];

	// TODO: Send success fail callback?
}

- (void)sendEventWithGuid:(NSString *)guid
{
	// TODO: nil check
 	TJEvent *event = [eventsDict_ objectForKey:guid];
 	[event setPresentationViewController:UnityGetGLViewController()];
	[event send];
}

- (void)enableEvent:(NSString *) guid autoPresent:(BOOL)autoPresent
{
	// TODO: nil check
	[[eventsDict_ objectForKey:guid] setPresentAutomatically: autoPresent];
}

- (void)enableEvent:(NSString *) guid preload:(BOOL)preload
{
	// TODO: nil check
	[[eventsDict_ objectForKey:guid] setPreload: preload];
}

- (void)eventRequestCompleted:(NSString *) guid
{
	TJEventRequest *eventRequest = [eventRequestDict_ objectForKey:guid];
	if(eventRequest)
	{
		NSLog(@"Sending TJEventRequest completed");
		[eventRequest completed];
	}
}

- (void)eventRequestCancelled:(NSString *) guid
{
	TJEventRequest *eventRequest = [eventRequestDict_ objectForKey:guid];
	if(eventRequest)
	{
		NSLog(@"Sending TJEventRequest cancelled");
		[eventRequest cancelled];
	}
}

- (void)presentEventWithGuid:(NSString *)guid
{
	// TODO: nil check
	[[eventsDict_ objectForKey:guid] presentContentWithViewController:UnityGetGLViewController()];
}

#pragma mark - Tapjoy Static Event Delegate Methods

- (void)sendEventComplete:(NSString *)guid withContent:(BOOL)contentIsAvailable
{
	if (contentIsAvailable)
		UnitySendMessage([callbackHandlerName_ UTF8String], "SendEventCompleteWithContent", [guid UTF8String]);
	else
		UnitySendMessage([callbackHandlerName_ UTF8String], "SendEventComplete", [guid UTF8String]);
}

- (void)sendEventFail:(NSString *)guid error:(NSError*)error
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "SendEventFail", [guid UTF8String]);
}

- (void)contentIsReady:(NSString *)guid withStatus:(int)status
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:guid];
	[parameters appendString:@","];
	[parameters appendString:[NSString stringWithFormat:@"%i", status]];
	
	UnitySendMessage([callbackHandlerName_ UTF8String], "ContentIsReady", [parameters UTF8String]);
}

- (void)contentWillAppear:(NSString *)guid
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ContentWillAppear", [guid UTF8String]);
}

- (void)contentDidAppear:(NSString *)guid
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ContentDidAppear", [guid UTF8String]);
}

- (void)contentWillDisappear:(NSString *)guid
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ContentWillDisappear", [guid UTF8String]);
}

- (void)contentDidDisappear:(NSString *)guid
{
	UnitySendMessage([callbackHandlerName_ UTF8String], "ContentDidDisappear", [guid UTF8String]);
}

- (void)event:(NSString *)guid didRequestAction:(TJEventRequest*)request
{
	// Create dictionary if its empty
	if (!eventRequestDict_)
		eventRequestDict_ = [[NSMutableDictionary alloc] init];
	
	[eventRequestDict_ setObject:request forKey:guid];
	
	//TODO: use json encoding
	NSString *message = [NSString stringWithFormat: @"%@,%d,%@,%d", guid, request.type, request.identifier, request.quantity];
	UnitySendMessage([callbackHandlerName_ UTF8String], "DidRequestAction", [message UTF8String]);
}

@end







// Converts C style string to NSString
NSString* tjCreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}


// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	

	void _SetCallbackHandler(const char* handlerName)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
		NSLog(@"callbackHandlerName: %@", [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] callbackHandlerName]);
	}
	
	void _RequestTapjoyConnect(const char* appID, const char* secretKey)
	{
		[Tapjoy setPlugin:@"unity"];
		
		NSDictionary* connectFlags = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:CONNECT_FLAG_KEY];
		if (connectFlags)
			[Tapjoy requestTapjoyConnect:tjCreateNSString(appID) secretKey:tjCreateNSString(secretKey) options:connectFlags];
		else
			[Tapjoy requestTapjoyConnect:tjCreateNSString(appID) secretKey:tjCreateNSString(secretKey)];
	}
  
	// Bridge methods between Objective-C and C#
	void _SetKeyToValueInDictionary(const char* key, const char* value, const char* dictionaryToAddTo)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setKey:tjCreateNSString(key) ToValue:tjCreateNSString(value) InDictionary:tjCreateNSString(dictionaryToAddTo)];
	}

	void _SetKeyToDictionaryRefValueInDictionary(const char* key, const char* dictionaryRefToAdd, const char* dictionaryToAddTo)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setKey:tjCreateNSString(key) ToDictionaryRefValue:tjCreateNSString(dictionaryRefToAdd) InDictionary:tjCreateNSString(dictionaryToAddTo)];
	}
	
	void _EnableLogging(bool enable)
	{
		[Tapjoy enableLogging:enable];
	}

	void _SendSegmentationParams(const char* dictionaryNameOfSegments)
	{
		[Tapjoy sendSegmentationParams:[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:tjCreateNSString(dictionaryNameOfSegments)]];
	}

	void _ActionComplete(const char* actionID)
	{
		[Tapjoy actionComplete:tjCreateNSString(actionID)];
	}
	
	
	void _SetUserID(const char* userID)
	{
		[Tapjoy setUserID:tjCreateNSString(userID)];
	}
	
	
	void _ShowOffers(void)
	{
		// Displays the offer wall.
		[Tapjoy showOffersWithViewController:UnityGetGLViewController()];
	}
    
    
    void _ShowOffersWithCurrencyID(const char* currencyID, bool isSelectorVisible)
    {
        // Displays the offer wall.
		[Tapjoy showOffersWithCurrencyID:tjCreateNSString(currencyID) withViewController:UnityGetGLViewController() withCurrencySelector:isSelectorVisible];
    }	
	

	void _GetFullScreenAd(void)
	{
		// Initiates a request to get fullscreen ad data.
		[Tapjoy getFullScreenAd];
	}
    
    
    void _GetFullScreenAdWithCurrencyID(const char* currencyID)
    {
        // Initiates a request to get fullscreen ad data.
		[Tapjoy getFullScreenAdWithCurrencyID:tjCreateNSString(currencyID)];
    }


    void _SetCurrencyMultiplier(float multiplier)
    {
    	// Sets the currency multiplier, and fires off a network call to notify the server.
    	[Tapjoy setCurrencyMultiplier:multiplier];
    }
	

	void _ShowFullScreenAd(void)
	{
		// Shows the default full screen fullscreen ad ad.
		[Tapjoy showFullScreenAdWithViewController:UnityGetGLViewController()];
	}
	
	void _ShowDefaultEarnedCurrencyAlert(void)
	{
		// Pops up a UIAlert notifying the user that they have successfully earned some currency.
		// This is the default alert, so you may place a custom alert here if you choose to do so.
		[Tapjoy showDefaultEarnedCurrencyAlert];
	}
	
	
	void _GetTapPoints(void)
	{	
		[Tapjoy getTapPoints];
	}
	
	
	void _SpendTapPoints(int points)
	{
		[Tapjoy spendTapPoints:points];
	}
	
	
	void _AwardTapPoints(int points)
	{	
		[Tapjoy awardTapPoints:points];
	}
	
	
	int _QueryTapPoints(void)
	{
		return [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] queryTapPoints];
	}
	
	
	void _GetDisplayAd(void)
	{
		[Tapjoy getDisplayAdWithDelegate:[TapjoyConnectPlugin sharedTapjoyConnectPlugin]];
	}
	

	void _ShowDisplayAd(void)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] showDisplayAd];
	}


	void _HideDisplayAd(void)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] hideDisplayAd];
	}
	

    void _GetDisplayAdWithCurrencyID(const char* currencyID)
	{
		[Tapjoy getDisplayAdWithDelegate:[TapjoyConnectPlugin sharedTapjoyConnectPlugin] currencyID:tjCreateNSString(currencyID)];
	}
    
    
	void _SetDisplayAdSize(const char* size)
	{
        NSString* adSize = tjCreateNSString(size);
    
        NSArray* sizeComponents = [adSize componentsSeparatedByString:@"x"];
        if (sizeComponents && sizeComponents.count == 2)
            [TapjoyConnectPlugin sharedTapjoyConnectPlugin].displayAdFrame = CGRectMake(0, 0, [sizeComponents[0] floatValue], [sizeComponents[1] floatValue]);
    
        [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setDisplayAdSize:adSize];
	}


	void _EnableDisplayAdAutoRefresh(bool enable)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setEnableDisplayAdAutoRefresh:enable];
	}
	
    
	void _MoveDisplayAd(int x, int y)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] moveDisplayAdToX:x toY:y];
	}
	
	
	void _SetTransitionEffect(int transition)
	{
		[Tapjoy setTransitionEffect:(TJCTransitionEnum)transition];
	}
	

	void _SendIAPEvent(const char*  name, float price, int quantity, const char*  currencyCode)
	{
		[Tapjoy sendIAPEvent:tjCreateNSString(name) price:price quantity:quantity currencyCode:tjCreateNSString(currencyCode)];
	}

	//#pragma mark - Tapjoy Event C Layer

	void _CreateEvent(const char* guid, const char* name, const char* param)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] createEventWithGuid:tjCreateNSString(guid) name:tjCreateNSString(name) parameter:tjCreateNSString(param)];
	}

	void _SendEvent(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] sendEventWithGuid:tjCreateNSString(guid)];
	}

	void _ShowEvent(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] presentEventWithGuid:tjCreateNSString(guid)];	
	}
  
  	void _EnableEventAutoPresent(const char* guid, bool autoPresent)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] enableEvent:tjCreateNSString(guid) autoPresent:autoPresent];
	}

	void _EnableEventPreload(const char* guid, bool preload)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] enableEvent:tjCreateNSString(guid) preload:preload];
	}

	void _EventRequestCompleted(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] eventRequestCompleted:tjCreateNSString(guid)];
	}

	void _EventRequestCancelled(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] eventRequestCancelled:tjCreateNSString(guid)];
	}
}


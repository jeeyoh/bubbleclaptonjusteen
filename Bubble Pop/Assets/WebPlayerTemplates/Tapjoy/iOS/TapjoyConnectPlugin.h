#import <Foundation/Foundation.h>
#import <Tapjoy/Tapjoy.h>

@interface TapjoyConnectPlugin : NSObject <TJCAdDelegate, TJCVideoAdDelegate, TJCViewDelegate>
{
	// The amount of Tapjoy Managed currency this user has.
	int tapPoints;
	
	// The amount of Tapjoy Managed currency this user has earned since the app was last run.
	int earnedCurrencyAmount;
	
	// This is used for callbacks to managed code, after Tapjoy events are fired.
	const char* gameObjectName;
	
	CGRect displayAdFrame_;
	
	// This is set to the name of the game object that will implement the callback functions for handling Tapjoy events.
	NSString *callbackHandlerName_;
	NSString *displayAdSize_;
	NSMutableDictionary *cSharpDictionaryRefs_;

	// Events
	NSMutableDictionary *eventsDict_;
	NSMutableDictionary *eventRequestDict_;
}

@property (nonatomic, retain) NSMutableDictionary *cSharpDictionaryRefs;
@property (nonatomic, copy) NSString *callbackHandlerName;
@property (nonatomic, copy) NSString *displayAdSize;
@property (nonatomic) UIInterfaceOrientation displayAdOrientation;
@property (nonatomic, assign, getter=shouldDisplayAdAutoRefresh) BOOL enableDisplayAdAutoRefresh;
@property (nonatomic, assign) CGRect displayAdFrame;
@property (nonatomic, retain) NSMutableDictionary *eventsDict;
@property (nonatomic, retain) NSMutableDictionary *eventRequestDict;

+ (TapjoyConnectPlugin*)sharedTapjoyConnectPlugin;

// Bridge methods between Objective-C and C#x
- (void)setKey:(NSString*)key ToValue:(NSString*)value InDictionary:(NSString*)dictionaryToAddTo;
- (void)setKey:(NSString*)key ToDictionaryRefValue:(NSString*)dictionaryRefToAdd InDictionary:(NSString*)dictionaryToAddTo;

- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName;
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName AndCreateNewInstance:(BOOL) newInstance;

// Tapjoy callback methods. Each one is triggered depending on responses from the server.
- (void)tjcConnectSuccess:(NSNotification*)notifyObj;
- (void)tjcConnectFail:(NSNotification*)notifyObj;

- (void)getUpdatedPoints:(NSNotification*)notifyObj;
- (void)getUpdatedPointsError:(NSNotification*)notifyObj;

- (void)spendPoints:(NSNotification*)notifyObj;
- (void)spendPointsError:(NSNotification*)notifyObj;

- (void)awardPoints:(NSNotification*)notifyObj;
- (void)awardPointsError:(NSNotification*)notifyObj;

- (void)getFullScreenAd:(NSNotification*)notifyObj;
- (void)getFullScreenAdError:(NSNotification*)notifyObj;

- (void)showEarnedCurrencyAlert:(NSNotification*)notifyObj;

- (void)showOffersError:(NSNotification*)notifyObj;

// After Tap Points status is set to TJC_STATUS_DONE, this can be called safely.
- (int)queryTapPoints;

// Moves the location of the banner ad.
- (void)moveDisplayAdToX:(int)x toY:(int)y;
- (void)showDisplayAd;
- (void)hideDisplayAd;
- (void)enableDisplayAdAutoRefresh:(BOOL)enable;

// Tapjoy Events
- (void)sendEventComplete:(NSString*)guid withContent:(BOOL)contentIsAvailable;
- (void)sendEventFail:(NSString*)guid error:(NSError*)error;
- (void)contentIsReady:(NSString*)guid withStatus:(int)status;
- (void)contentWillAppear:(NSString*)guid;
- (void)contentDidAppear:(NSString*)guid;
- (void)contentWillDisappear:(NSString*)guid;
- (void)contentDidDisappear:(NSString*)guid;
- (void)event:(NSString*)guid didRequestAction:(TJEventRequest*)request;

@end


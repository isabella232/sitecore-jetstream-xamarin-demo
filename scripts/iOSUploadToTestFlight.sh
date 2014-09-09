LAUNCH_DIR=$PWD
RELEASE_DIR="$LAUNCH_DIR/JetStreamIOS/JetStreamIOS/bin/iPhone/Release"
IPA_FILE="$RELEASE_DIR/JetStreamIOS.ipa"

function build_application
{
	/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool -v build "--configuration:Release|iPhone" Solutions/JetStreamIOS.sln
}

function make_ipa_file 
{
	cd $RELEASE_DIR

	mkdir -p "Payload"
	cp -r  "JetStreamIOS.app" "Payload/JetStreamIOS.app"
	zip $IPA_FILE -r "Payload"
	
	cd "$LAUNCH_DIR"
}

function upload_ipa_to_testflight 
{
	cd $RELEASE_DIR
	
    curl http://testflightapp.com/api/builds.json \
    -F file=@$IPA_FILE \
    -F api_token='a3620a9760dc97411328bc73864c9a82_Mzk2NDIxMjAxMi0wNC0xMyAwNDowNzozMS42MDQ2NTQ' \
    -F team_token='9a4cf26c2ba6dab0ae956567f92fc0c0_ODA1NzEyMDEyLTA0LTEzIDA0OjEzOjM5LjA5OTQ0Nw' \
    -F notes='Built by Hudson' \
    -F notify=True \
    -F distribution_lists='Mobile iOS Team' \
    -F dsym=@$DSYM_FILE
	
	cd "$LAUNCH_DIR"
}

build_application
make_ipa_file
upload_ipa_to_testflight
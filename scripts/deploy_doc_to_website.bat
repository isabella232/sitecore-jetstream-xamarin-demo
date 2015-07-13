set website_root_directory="C:\dev\jetstream_doc"
set project_name="Jetstream"
set project_version="2.0"
set output_directory="generated_doc_webiste"
set features_directory="doc\Features"
set changes_file_name="changes.txt"
set images_directory="doc\images"

:: Generating website pages from feature files
:: format 
scripts\pickles\Pickles.exe -feature-directory=%features_directory% -output-directory=%output_directory% -df=DHTML -sn=%project_name% -sv=%project_version%

:: Copying feature images from workspace into website /img folder.
xcopy /e /i %images_directory%\* %output_directory%\img\

::Cleaning website root except version file.
:: I used powershell here because I can't imagine how to implement this with batch.
powershell -noprofile -executionpolicy RemoteSigned -file scripts\clean_website_root.ps1 %website_root_directory% %changes_file_name%

:: Copying website pages from workspace into website root.
xcopy /e /i %output_directory% %website_root_directory%

echo ------------------------^

Commit hash : %GIT_COMMIT%^

Build ID : %BUILD_ID%^

Build URL: %BUILD_URL% >>%website_root_directory%\%changes_file_name% 

"C:\Program Files\7-Zip\7zG.exe" a -r -tzip %output_directory%\webiste.zip %output_directory%\*.*


#!/bin/sh
datetime=$(date +%Y%m%d_%H%M%S)
echo "Add commit message [ENTER]:"
read text
message="${text}_${datetime}"
git add -u
git commit -m $message
git push
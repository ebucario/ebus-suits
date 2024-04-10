#!/usr/bin/env bash

if [[ ! -r "manifest.json" ]]; then
	echo "couldn't find manifest.json... are you in the right directory?"
	exit 1;
fi

mkdir -p "build"

ZIP="build/ebus_suits.$(jq -r .version_number manifest.json).zip"

if [[ -r $ZIP ]]; then
	rm "$ZIP"
fi

zip -r "$ZIP" BepInEx CHANGELOG.md icon.png manifest.json README.md

echo "done"
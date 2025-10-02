#!/bin/bash

#######
# Config ZSH
#######
# powerline fonts for zsh theme
git clone https://github.com/powerline/fonts.git
cd fonts
./install.sh
cd .. && rm -rf fonts

# oh-my-zsh plugins
zsh -c 'git clone --depth=1 https://github.com/romkatv/powerlevel10k.git ${ZSH_CUSTOM:-~/.oh-my-zsh/custom}/themes/powerlevel10k'
cp /scripts/dotfiles/.zshrc ~
cp /scripts/dotfiles/.p10k.zsh ~

# Update the workloads
sudo dotnet workload update

# EF Tools
dotnet tool install -g dotnet-ef

#######
# Done
#######
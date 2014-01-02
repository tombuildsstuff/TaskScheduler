# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
    config.vm.box = "Ubuntu precise 64 VMWare"
    config.vm.box_url = "http://files.vagrantup.com/precise64_vmware.box"
    config.vm.network :forwarded_port, guest: 8000, host: 8080
    config.vm.synced_folder ".", "/source"
    config.vm.provision :shell, :path => "setup/bootstrap.sh"
end 
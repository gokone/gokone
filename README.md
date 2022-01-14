![gokonelogo-250x250](https://user-images.githubusercontent.com/13405205/144325971-b2aa727c-3648-41f1-b5e9-0359b7a5ec8c.png)

# GokOne

P2P Cricket Bets using bitcoin

### Available features:

- Create offer: broadcast public key
- Get offer: get public keys from offer transactions
- Accept offer: create 2of2 multisig with 2 public keys

### How to use:

:information_source: Ensure that `bitcoind` settings are set correctly while using scripts. Apart from things mentioned in comments for each script and variables, save below things in _bitcoin.conf_ before running application or scripts:

1. Run `bitcoind`

2. Change RPC credentials accordingly

3. Open solution in Visual Studio and run

### Things to do:

- Implement discreet log contracts using [`bitcoin-s`](https://bitcoin-s.org/docs/0.5.0/wallet/dlc) RPC

:question: If you have any questions and ideas, please feel free to create issues or pull requests.

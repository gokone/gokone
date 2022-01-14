![gokonelogo-250x250](https://user-images.githubusercontent.com/13405205/144325971-b2aa727c-3648-41f1-b5e9-0359b7a5ec8c.png)

# GokOne

P2P Cricket Bets using bitcoin

![image](https://user-images.githubusercontent.com/13405205/149520338-2333034e-e9ef-43fa-97c4-4771266b58c4.png)


### Available features:

- Create offer: broadcast public key
- Get offer: get public keys from offer transactions
- Accept offer: create 2of2 multisig with 2 public keys

### How to use:

:information_source: Change _json_path_ until a default location is used for saving offer addresses

1. Run `bitcoind`

2. Change RPC credentials accordingly

3. Open solution in Visual Studio and run

### Things to do:

- Implement discreet log contracts using [`bitcoin-s`](https://bitcoin-s.org/docs/0.5.0/wallet/dlc) RPC
- Use Avalonia to make the application cross platform
- Use Wasabi RPC for coinjoin (optional)
- Improve offerbook and most probably use GNUnet API in future

:question: If you have any questions and ideas, please feel free to create issues or pull requests.

import {
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType,
} from '@microsoft/signalr'

let connection = null

if (connection == null) {
  connection = new HubConnectionBuilder()
    .withUrl('https://localhost:7202/notification/producthub', {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets,
    })
    .configureLogging(LogLevel.Information)
    .build()

  connection.start()
  console.log('Connected...')
}

function subscribeToProduct(productId) {
  if (connection == null) return

  connection.invoke('subscribeToProduct', productId)
}

function unsubscribeToProduct(productId) {
  if (connection == null) return

  connection.invoke('unsubscribeToProduct', productId)
}

export {
  connection,
  subscribeToProduct,
  unsubscribeToProduct
}

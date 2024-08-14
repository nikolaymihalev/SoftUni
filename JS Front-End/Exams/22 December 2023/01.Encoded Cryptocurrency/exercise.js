function solution(input) {
    let message = input.shift();

    let action = {
        TakeEven() {
            let newMessage = '';
            for (let i = 0; i < message.length; i += 2) {
                newMessage += message[i];
            }

            message = newMessage;

            console.log(message);
        },
        ChangeAll(substring, replacement) {
            while (message.includes(substring)) {
                message = message.replace(substring, replacement);
            }

            console.log(message);
        },
        Reverse(substring) {
            if (message.includes(substring)) {
                message = message.replace(substring, '');

                let newSub = substring.split('').reverse().join('');

                message += newSub;

                console.log(message);

            } else {
                console.log('error');
            }
        }
    }

    let command = input.shift();
    while (command !== "Buy") {
        const [actionName, ...args] = command.split('?');

        action[actionName](...args);

        command = input.shift();
    }

    console.log(`The cryptocurrency is: ${message}`);

}    
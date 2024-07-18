function solve(input) {
    class  Cat{
        constructor(name, age){
            this.name = name;
            this.age = age;
        }

        meow(){
            console.log(`${this.name}, age ${this.age} says Meow`);
        }
    }


    input.map(entry => entry.split(' '))
        .map(([name, age]) => new Cat(name,age))
        .forEach(cat => cat.meow()        
    );
}
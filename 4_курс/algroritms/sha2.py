# SHA-256
# https://blog.pagefreezer.com/sha-256-benefits-evidence-authentication#:~:text=The%20main%20reason%20technology%20leaders,some%20other%20popular%20hashing%20algorithms. 
class Bin32:
    MOD = 2**32

    def __init__(self, value: int):
        self.value = value % self.MOD

    def to_str(self):
        return str(bin(self.value)[2:]).zfill(32)

    def __sub__(self, n: int) -> "Bin32":
        """Rotate right"""
        s = bin(self.value)[2:].zfill(32)
        return Bin32(int(s[-n:] + s[:-n], 2))

    def __repr__(self) -> str:
        return self.to_str()

    def __rshift__(self, n: int) -> "Bin32":
        return Bin32(self.value >> n)

    def __add__(self, other: "Bin32") -> "Bin32":
        return Bin32(self.value + other.value)

    def __xor__(self, other: "Bin32") -> "Bin32":
        return Bin32(self.value ^ other.value)

    def __and__(self, other: "Bin32") -> "Bin32":
        return Bin32(self.value & other.value)

    def __invert__(self):
        return Bin32(
            int("".join((str(1 if int(x) == 0 else 0) for x in self.to_str())), 2)
        )

    def to_hex(self):
        return hex(self.value)[2:].zfill(8)


def sha_256(text_original="hello world") -> str:
    ### Step 1. Data preprocessing
    # 1. convert to binary
    text = "".join(format(x, "08b") for x in bytearray(text_original, "utf-8"))
    # 2. add one to the end
    text += "1"
    # 3. add zeros while not mod 512 = 0 without last 64 bits
    text += "0" * (512 - len(text) % 512)
    text = text[:-64]
    assert len(text) % 512 == 448
    # 4. add length text_original in last 64 bit big-endian
    end = format(len(text_original) * 8, "08b")
    if len(end) < 64:
        end = "0" * (64 - len(end)) + end
    text += end

    ### Step 2. Initialization of hash values
    h0 = Bin32(0x6A09E667)
    h1 = Bin32(0xBB67AE85)
    h2 = Bin32(0x3C6EF372)
    h3 = Bin32(0xA54FF53A)
    h5 = Bin32(0x9B05688C)
    h4 = Bin32(0x510E527F)
    h6 = Bin32(0x1F83D9AB)
    h7 = Bin32(0x5BE0CD19)

    ### Step 3. Initialization of rounded constants
    # fmt: off
    K = [
        Bin32(0x428a2f98), Bin32(0x71374491), Bin32(0xb5c0fbcf), Bin32(0xe9b5dba5), Bin32(0x3956c25b), Bin32(0x59f111f1), Bin32(0x923f82a4), Bin32(0xab1c5ed5),
        Bin32(0xd807aa98), Bin32(0x12835b01), Bin32(0x243185be), Bin32(0x550c7dc3), Bin32(0x72be5d74), Bin32(0x80deb1fe), Bin32(0x9bdc06a7), Bin32(0xc19bf174),
        Bin32(0xe49b69c1), Bin32(0xefbe4786), Bin32(0x0fc19dc6), Bin32(0x240ca1cc), Bin32(0x2de92c6f), Bin32(0x4a7484aa), Bin32(0x5cb0a9dc), Bin32(0x76f988da),
        Bin32(0x983e5152), Bin32(0xa831c66d), Bin32(0xb00327c8), Bin32(0xbf597fc7), Bin32(0xc6e00bf3), Bin32(0xd5a79147), Bin32(0x06ca6351), Bin32(0x14292967),
        Bin32(0x27b70a85), Bin32(0x2e1b2138), Bin32(0x4d2c6dfc), Bin32(0x53380d13), Bin32(0x650a7354), Bin32(0x766a0abb), Bin32(0x81c2c92e), Bin32(0x92722c85),
        Bin32(0xa2bfe8a1), Bin32(0xa81a664b), Bin32(0xc24b8b70), Bin32(0xc76c51a3), Bin32(0xd192e819), Bin32(0xd6990624), Bin32(0xf40e3585), Bin32(0x106aa070),
        Bin32(0x19a4c116), Bin32(0x1e376c08), Bin32(0x2748774c), Bin32(0x34b0bcb5), Bin32(0x391c0cb3), Bin32(0x4ed8aa4a), Bin32(0x5b9cca4f), Bin32(0x682e6ff3),
        Bin32(0x748f82ee), Bin32(0x78a5636f), Bin32(0x84c87814), Bin32(0x8cc70208), Bin32(0x90befffa), Bin32(0xa4506ceb), Bin32(0xbef9a3f7), Bin32(0xc67178f2)
    ]
    # fmt: on

    ### Step 4. Main cycle
    # every block of 512 bits, we have only one
    for chunk in range(0, len(text), 512):
        ### Step 5. Queue of messages
        # 1. Split to 32 bit words
        w = [Bin32(int(text[i : i + 32], 2)) for i in range(chunk, chunk + 512, 32)]
        # 2. Adding 48 zeros 32 bit words
        w.extend([Bin32(0) for i in range(48)])
        # 3. Doing some magic stuff - changing words =)
        for i in range(16, 64):
            s0 = (w[i - 15] - 7) ^ (w[i - 15] - 18) ^ (w[i - 15] >> 3)
            s1 = (w[i - 2] - 17) ^ (w[i - 2] - 19) ^ (w[i - 2] >> 10)
            w[i] = w[i - 16] + s0 + w[i - 7] + s1

        ### Step 6. Compression
        a = h0
        b = h1
        c = h2
        d = h3
        e = h4
        g = h6
        f = h5
        h = h7

        for i in range(64):
            s1 = (e - 6) ^ (e - 11) ^ (e - 25)
            ch = (e & f) ^ (~e & g)
            temp1 = h + s1 + ch + K[i] + w[i]
            s0 = (a - 2) ^ (a - 13) ^ (a - 22)
            maj = (a & b) ^ (a & c) ^ (b & c)
            temp2 = s0 + maj
            h = g
            g = f
            f = e
            e = d + temp1
            d = c
            c = b
            b = a
            a = temp1 + temp2
        ### Step 7. Modify Final Values
        h0 = h0 + a
        h1 = h1 + b
        h2 = h2 + c
        h3 = h3 + d
        h4 = h4 + e
        h5 = h5 + f
        h6 = h6 + g
        h7 = h7 + h

    ### Step 8. Concatenate Final Hash
    return "".join(
        (
            x.to_hex()
            for x in (
                h0,
                h1,
                h2,
                h3,
                h4,
                h5,
                h6,
                h7,
            )
        )
    )


def main():
    text = input("Enter message: ")
    from hashlib import sha256

    library = sha256(text.encode("utf-8")).hexdigest()
    my = sha_256(text)
    print("Library hash   : ", library)
    print("Hash by guide  : ", my)


if __name__ == "__main__":
    main()
